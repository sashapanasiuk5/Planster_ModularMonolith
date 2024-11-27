using FluentResults;
using Infrastructure.EventBus;
using MediatR;
using Shared.Contracts.IntegrationEvents;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Commands.CreateTask;

public class CreateTaskCommandHandler: IRequestHandler<CreateTaskCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }
    public async Task<Result<Unit>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var status = await _unitOfWork.TaskRepository.GetStatusByIdAsync(request.task.StatusId);
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(request.task.ProjectId);
        
        if(status == null)
            return Result.Fail("Status not found");
        if(project == null)
            return Result.Fail("Project not found");
        
        var newTask = request.task.ToModel(project);
        newTask.ChangeStatus(status);
        if (request.task.AssigneeId != null)
        {
            var assignee = await _unitOfWork.AssigneeRepository.GetByIdAsync(request.task.AssigneeId.Value);
            if(assignee == null)
                return Result.Fail("Assignee not found");
            newTask.SetAssignee(assignee);
        }
        
        if (request.task.ParentTaskId != null)
        {
            var parentTask = await _unitOfWork.TaskRepository.GetByIdAsync(request.task.ParentTaskId.Value);
            if(parentTask == null)
                return Result.Fail($"Parent with id {request.task.ParentTaskId} task not found");
            
            var result = newTask.AddParentTask(parentTask);

            if (result.IsFailed)
            {
                return result;
            }
        }

        foreach (var taskId in request.task.SubTasksIdsToAdd)
        {
            var subTask = await _unitOfWork.TaskRepository.GetWithHierarchyByIdAsync(taskId);
            if(subTask == null)
                return Result.Fail($"Task with id {taskId} not found");
            var result = newTask.AddSubTask(subTask);
            
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
            newTask.AddToSprint(sprint);
        }
        
        _unitOfWork.TaskRepository.AddTask(newTask);
        await _unitOfWork.SaveAsync();
        await _eventBus.PublishAsync(new TaskStatusChanged(Guid.NewGuid(), newTask.Id, status.Id));
        return Result.Ok();
    }
}