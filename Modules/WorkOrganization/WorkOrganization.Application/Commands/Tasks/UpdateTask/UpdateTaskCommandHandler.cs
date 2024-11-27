using FluentResults;
using Infrastructure.EventBus;
using MediatR;
using Shared.Contracts.IntegrationEvents;
using WorkOrganization.Application.Interfaces;

namespace WorkOrganization.Application.Commands.UpdateTask;

public class UpdateTaskCommandHandler: IRequestHandler<UpdateTaskCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }
    public async Task<Result<Unit>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.TaskRepository.GetByIdWithDetailsAsync(request.taskId);
        if(task is null)
            return Result.Fail("Task not found");
        
        var status = await _unitOfWork.TaskRepository.GetStatusByIdAsync(request.task.StatusId);
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(request.task.ProjectId);
        
        if(status == null)
            return Result.Fail("Status not found");
        if(project == null)
            return Result.Fail("Project not found");
        
        task.Update(request.task.Code,
                    request.task.Title,
                    request.task.Description,
                    request.task.AcceptanceCriteria,
                    request.task.Priority,
                    request.task.Estimation,
                    request.task.Type);
        
        var isStatusChanged = task.Status.Id != request.task.StatusId;
        if (isStatusChanged)
        {
            task.ChangeStatus(status);
        }
        
        if (request.task.AssigneeId != null)
        {
            var assignee = await _unitOfWork.AssigneeRepository.GetByIdAsync(request.task.AssigneeId.Value);
            if(assignee == null)
                return Result.Fail("Assignee not found");
            task.SetAssignee(assignee);
        }
        
        if (request.task.ParentTaskId != null)
        {
            var parentTask = await _unitOfWork.TaskRepository.GetByIdAsync(request.task.ParentTaskId.Value);
            if(parentTask == null)
                return Result.Fail($"Parent with id {request.task.ParentTaskId} task not found");
            
            var result = task.AddParentTask(parentTask);

            if (result.IsFailed)
            {
                return result;
            }
        }
        else
        {
            task.RemoveParentTask();
        }

        foreach (var taskId in request.task.SubTasksIdsToAdd)
        {
            var subTask = await _unitOfWork.TaskRepository.GetWithHierarchyByIdAsync(taskId);
            if(subTask == null)
                return Result.Fail($"Task with id {taskId} not found");
            
            var result = task.AddSubTask(subTask);
            
            if (result.IsFailed)
            {
                return result;
            }
        }

        foreach (var subTaskId in request.task.SubTasksIdsToRemove)
        {
            var result = task.RemoveSubTaskById(subTaskId);
            if (result.IsFailed)
            {
                return result;
            }
        }

        if (request.task.SprintId != null)
        {
            var sprint = await _unitOfWork.SprintRepository.GetByIdAsync(request.task.SprintId.Value);
            if(sprint == null)
                return Result.Fail("Sprint not found");
            task.AddToSprint(sprint);
        }
        else
        {
            task.RemoveFromSprint();
        }
        
        await _unitOfWork.SaveAsync();
        if(isStatusChanged)
            await _eventBus.PublishAsync(new TaskStatusChanged(Guid.NewGuid(), task.Id, status.Id));
        return Result.Ok();
    }
}