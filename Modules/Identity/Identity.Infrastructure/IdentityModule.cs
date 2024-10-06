using Application.Commands.AddIdentity;
using Application.Commands.GetIdentityBySession;
using FluentResults;
using Infrastructure.Dto;
using Infrastructure.ModulesInterfaces;
using MediatR;

namespace Infrastructure;

public class IdentityModule: IIdentityModule
{
    private readonly IMediator _mediator;

    public IdentityModule(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<SessionDto> AddNewIdentityAsync(NewUserDto user)
    {
        var result = await _mediator.Send(new AddIdentityCommand(user));
        return result.Value;
    }

    public async Task<int?> GetIdentityAsync(string sessionId)
    {
        var result = await _mediator.Send(new GetIdentityBySessionCommand(sessionId));
        if(result.IsSuccess)
            return result.Value;
        return null;
    }
}