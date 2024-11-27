using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Tasks;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;

namespace WorkOrganization.Application.Commands.Tasks.FindTaskByTitle;

public class FindTaskByTitleCommandHandler: IRequestHandler<FindTaskByTitleCommand, Result<List<TaskShortDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public FindTaskByTitleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<TaskShortDto>>> Handle(FindTaskByTitleCommand request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.TaskRepository.FindTasksByTileAsync(request.ProjectId, request.SearchWord);
        return Result.Ok(tasks.Select(x => x.ToShortDto()).ToList());
    }
}