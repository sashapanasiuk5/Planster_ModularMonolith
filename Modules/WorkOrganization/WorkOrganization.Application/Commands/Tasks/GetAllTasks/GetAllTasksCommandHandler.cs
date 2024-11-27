using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;

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
        var dtos = tasks.Select(x => x.ToHierarchyDto());
        return Result.Ok(dtos.ToList());
    }
}