using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Tasks;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;

namespace WorkOrganization.Application.Commands.Tasks.GetTasksOnBoard;

public class GetTasksOnBoardCommandHandler: IRequestHandler<GetTasksOnBoardCommand, Result<List<BoardTaskDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTasksOnBoardCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<BoardTaskDto>>> Handle(GetTasksOnBoardCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _unitOfWork.SprintRepository.GetCurrentSprintAsync(request.ProjectId);
        if(sprint is null)
            return Result.Fail("Sprint not found");
        var tasks = await _unitOfWork.TaskRepository.GetOnlyTasksInSprintAsync(sprint.Id);
        return Result.Ok(tasks.Select( x => x.ToBoardDto()).ToList());
    }
}