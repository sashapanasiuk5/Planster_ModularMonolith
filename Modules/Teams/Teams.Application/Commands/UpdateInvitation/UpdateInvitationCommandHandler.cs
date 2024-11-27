using FluentResults;
using MediatR;
using Teams.Application.Interfaces;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.UpdateInvitation;

public class UpdateInvitationCommandHandler: IRequestHandler<UpdateInvitationCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInvitationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(UpdateInvitationCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.ProjectId);
        if (project == null)
            return Result.Fail(new ProjectNotFound(request.ProjectId));

        var invitation = project.FindInvitationById(request.Id);
        if(invitation == null)
            return Result.Fail(new InvitationNotFound(request.Id));
        invitation.Update(request.Invitation.RoleId, request.Invitation.NumberOfPlaces);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }
}