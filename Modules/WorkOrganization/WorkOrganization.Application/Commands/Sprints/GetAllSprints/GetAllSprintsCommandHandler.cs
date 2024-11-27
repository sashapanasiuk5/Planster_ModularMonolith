using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Work.Sprints;
using WorkOrganization.Application.Interfaces;
using WorkOrganization.Application.Mappers;

namespace WorkOrganization.Application.Commands.Sprints.GetAllSprints;

public class GetAllSprintsCommandHandler: IRequestHandler<GetAllSprintsCommand, Result<List<SprintShortDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSprintsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<SprintShortDto>>> Handle(GetAllSprintsCommand request, CancellationToken cancellationToken)
    {
        var sprints = await _unitOfWork.SprintRepository.GetAllSprintsAsync(request.ProjectId);
        var dto = sprints.Select(x => x.ToShortDto()).ToList();
        return Result.Ok(dto);
    }
}