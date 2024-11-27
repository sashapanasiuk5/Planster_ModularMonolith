using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;
using Teams.Domain.Interfaces;
using Teams.Domain.Models;

namespace Teams.Application.Commands.CreateInvitation;

public class CreateInvitationCommandHandler: IRequestHandler<CreateInvitationCommand, Result<InvitationDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRandomStringGenerator _randomStringGenerator;

    public CreateInvitationCommandHandler(IUnitOfWork unitOfWork, IRandomStringGenerator randomStringGenerator)
    {
        _unitOfWork = unitOfWork;
        _randomStringGenerator = randomStringGenerator;
    }
    public async Task<Result<InvitationDto>> Handle(CreateInvitationCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.ProjectId);
        if (project == null)
        {
            return Result.Fail(new ProjectNotFound(request.ProjectId));
        }
        var invitation = new Invitation(project, request.Dto.RoleId, request.Dto.NumberOfPlaces, _randomStringGenerator);
        project.AddInvitation(invitation);
        _unitOfWork.ProjectsRepository.AddInvitation(invitation);
        await _unitOfWork.SaveChangesAsync();
        return invitation.ToDto();
    }
}