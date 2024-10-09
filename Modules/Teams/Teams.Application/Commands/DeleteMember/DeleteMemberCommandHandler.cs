using System.Security.Principal;
using FluentResults;
using MediatR;
using Shared.Contracts.ModulesInterfaces;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.DeleteMember;

public class DeleteMemberCommandHandler: IRequestHandler<DeleteMemberCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityModule _identityModule;

    public DeleteMemberCommandHandler(IUnitOfWork unitOfWork, IIdentityModule identityModule)
    {
        _unitOfWork = unitOfWork;
        _identityModule = identityModule;
    }
    public async Task<Result<Unit>> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MembersRepository.GetMemberByIdAsync(request.MemberId);
        if(member == null)
            return Result.Fail(new MemberNotFound(request.MemberId));
        var project = await _unitOfWork.ProjectsRepository.GetProjectById(request.ProjectId);
        if(project == null)
            return Result.Fail(new ProjectNotFound(request.ProjectId));
        var result = project.RemoveMember(member);
        if (result.IsSuccess)
        {
            await _identityModule.UpdatePermissionsAsync(member.Id,
                member.ProjectMembers.Select(x => x.ToPermissionDto()).ToList());
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }

        return result;
    }
}