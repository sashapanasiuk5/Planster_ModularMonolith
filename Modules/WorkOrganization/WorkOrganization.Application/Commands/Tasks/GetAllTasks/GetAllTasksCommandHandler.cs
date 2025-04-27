using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Commands.GetAllTasks;

public class GetAllTasksCommandHandler: IRequestHandler<GetAllTasksCommand, Result<List<TaskHierarchyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTasksCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<TaskHierarchyDto>>> Handle(GetAllTasksCommand request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.TaskRepository.GetAllWithHierarchyAsync(request.ProjectId, request.Filter);
        
        List<ProjectTask> withoutDublicates = tasks.Where(task => tasks.All(x => x.Id != task.ParentTaskId)).ToList();

        var dtos = withoutDublicates.Select(x => x.ToHierarchyDto());
        return Result.Ok(dtos.ToList());
    }
}