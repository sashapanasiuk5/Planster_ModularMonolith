using FluentResults;
using MediatR;
using Shared.Contracts.Errors;
using Shared.Contracts.ModulesInterfaces;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.AcceptInvitation;

public class AcceptInvitationCommandHandler: IRequestHandler<AcceptInvitationCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityModule _identityModule;

    public AcceptInvitationCommandHandler(IUnitOfWork unitOfWork, IIdentityModule identityModule)
    {
        _unitOfWork = unitOfWork;
        _identityModule = identityModule;
    }
    public async Task<Result<Unit>> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.projectId);
        var member = await _unitOfWork.MembersRepository.GetMemberByIdAsync(request.memberId);
        if(project == null)
            return Result.Fail(new ProjectNotFound(request.projectId));
        if (member == null)
            return Result.Fail(new MemberNotFound(request.memberId));
        var invitation = project.Invitations.FirstOrDefault(i => i.Code == request.dto.Code);
        if (invitation == null)
            return Result.Fail(new InvalidInvitationCode(request.dto.Code));

        var result = invitation.Accept(member);
        if (result.IsSuccess)
        {
            var newProjectMember = result.Value;
            member.JoinProject(newProjectMember);
            _unitOfWork.ProjectsRepository.AddNewProjectMember(newProjectMember);
            await _identityModule.UpdatePermissionsAsync(member.Id, member.ProjectMembers.Select(pm => pm.ToPermissionDto()).ToList());
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }
        return Result.Fail(result.Errors);
    }
}