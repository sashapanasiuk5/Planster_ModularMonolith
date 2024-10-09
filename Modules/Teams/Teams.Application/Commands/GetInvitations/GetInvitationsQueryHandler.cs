using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.GetInvitations;

public class GetInvitationsQueryHandler: IRequestHandler<GetInvitationsQuery, Result<List<InvitationDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetInvitationsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<InvitationDto>>> Handle(GetInvitationsQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.ProjectId);
        if(project == null)
            return Result.Fail(new ProjectNotFound(request.ProjectId));

        var invitations = project.Invitations;
        return Result.Ok(invitations.Select(x => x.ToDto()).ToList());
    }
}