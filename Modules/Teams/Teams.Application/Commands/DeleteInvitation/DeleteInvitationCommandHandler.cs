using FluentResults;
using MediatR;
using Teams.Application.Interfaces;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.DeleteInvitation;

public class DeleteInvitationCommandHandler: IRequestHandler<DeleteInvitationCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInvitationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Unit>> Handle(DeleteInvitationCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.ProjectId);
        if(project == null)
            return Result.Fail(new ProjectNotFound(request.ProjectId));
        
        var invitation = project.FindInvitationById(request.InvitationId);
        if (invitation == null)
            return Result.Fail(new InvitationNotFound(request.InvitationId));
        project.DeleteInvitation(invitation);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }
}