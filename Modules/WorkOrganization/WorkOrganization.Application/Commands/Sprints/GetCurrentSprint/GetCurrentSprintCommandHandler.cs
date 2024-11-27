using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Sprints;
using Shared.Contracts.Dto.Work.Tasks;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;

namespace WorkOrganization.Application.Commands.Sprints.GetCurrentSprint;

public class GetCurrentSprintCommandHandler: IRequestHandler<GetCurrentSprintCommand, Result<SprintDto?>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCurrentSprintCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<SprintDto?>> Handle(GetCurrentSprintCommand request, CancellationToken cancellationToken)
    {
        var sprint = await _unitOfWork.SprintRepository.GetCurrentSprintAsync(request.ProjectId);
        if (sprint == null)
            return Result.Ok<SprintDto?>(null);

        var tasks = await _unitOfWork.TaskRepository.GetAllWithHierarchyAsync(request.ProjectId,
            new TaskFilterDto() { SprintId = sprint.Id });
        
        var tasksDtos = tasks.Select(x => x.ToHierarchyDto()).ToList();
        var dto = sprint.ToDto(tasksDtos);
        return Result.Ok<SprintDto?>(dto);
    }
}