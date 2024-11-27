using FluentResults;
using MediatR;
using Teams.Domain.Errors;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;
using WorkOrganization.Domain.Errors;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Commands.Sprints.CreateSprint;

public class CreateSprintCommandHandler: IRequestHandler<CreateSprintCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSprintCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(request.ProjectId);
        if (project == null)
            return Result.Fail(new ProjectNotFound(request.ProjectId));

        var currentSprint = await _unitOfWork.SprintRepository.GetCurrentSprintAsync(request.ProjectId);

        if (currentSprint != null)
            return Result.Fail(new SprintAlreadyExists());

        var sprint = request.Sprint.ToModel(project);
        
        foreach (var id in request.Sprint.TasksIdsToAdd)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(id);
            if(task == null)
                return Result.Fail("Task not found");
            sprint.AddTask(task);
        }
        _unitOfWork.SprintRepository.Add(sprint);
        await _unitOfWork.SaveAsync();
        return Result.Ok();
    }
}