using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Tasks;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;
using WorkOrganization.Domain.Models;

namespace WorkOrganization.Application.Commands.Tasks.FindTasksWithHierarchy;

public class FindTasksWithHierarchyCommandHandler: IRequestHandler<FindTasksWithHierarchyCommand, Result<List<TaskShortHierarchyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public FindTasksWithHierarchyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<TaskShortHierarchyDto>>> Handle(FindTasksWithHierarchyCommand request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.TaskRepository.RecursiveFindTasks(request.ProjectId, request.SearchWord);
        return Result.Ok(tasks.Select(x => x.ToShortHierarchyDto()).ToList());
    }
}