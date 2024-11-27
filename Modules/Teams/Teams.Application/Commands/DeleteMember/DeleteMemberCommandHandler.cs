using System.Security.Principal;
using FluentResults;
using Infrastructure.EventBus;
using MediatR;
using Shared.Contracts.IntegrationEvents;
using Shared.Contracts.ModulesInterfaces;
using Teams.Application.Interfaces;
using Teams.Application.Mappers;
using Teams.Domain.Errors;

namespace Teams.Application.Commands.DeleteMember;

public class DeleteMemberCommandHandler: IRequestHandler<DeleteMemberCommand, Result<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityModule _identityModule;
    private readonly IEventBus _bus;

    public DeleteMemberCommandHandler(IUnitOfWork unitOfWork, IIdentityModule identityModule, IEventBus bus)
    {
        _unitOfWork = unitOfWork;
        _identityModule = identityModule;
        _bus = bus;
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
            await _bus.PublishAsync(new TeamMemberDeleted(Guid.NewGuid(), member.Id, project.Id), cancellationToken);
            return Result.Ok();
        }

        return result;
    }
}