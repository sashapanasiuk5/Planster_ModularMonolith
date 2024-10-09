using Application.Commands.AddIdentity;
using Application.Commands.GetIdentityBySession;
using Application.Commands.UpdatePermissions;
using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;
using Shared.Contracts.ModulesInterfaces;
using Users.Contracts.Dto;

namespace Infrastructure;

public class IdentityModule: IIdentityModule
{
    private readonly IMediator _mediator;

    public IdentityModule(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<SessionDto> AddNewIdentityAsync(NewUserDto user, int userId)
    {
        var result = await _mediator.Send(new AddIdentityCommand(userId, user));
        return result.Value;
    }

    public async Task<SessionDto?> GetSessionAsync(string sessionId)
    {
        var result = await _mediator.Send(new GetSessionCommand(sessionId));
        if (result.IsSuccess)
        {
            return result.Value;
        }

        return null;
    }

    public async Task UpdatePermissionsAsync(int userId, List<MemberPermissionDto> permissions)
    {
        await _mediator.Send(new UpdatePermissionsCommand(userId, permissions));
    }
}