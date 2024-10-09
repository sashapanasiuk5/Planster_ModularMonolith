using Application.Interfaces;
using Application.Mappers;
using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using MediatR;

namespace Application.Commands.GetIdentityBySession;

public class GetSessionCommandHandler: IRequestHandler<GetSessionCommand, Result<SessionDto>>
{
    private readonly ISessionStore _sessionStore;

    public GetSessionCommandHandler(ISessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }
    public async Task<Result<SessionDto>> Handle(GetSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionStore.GetById(request.sessionId);
        if (session != null)
        {
            return Result.Ok(session.ToDto());
        }
        return Result.Fail("Session not found");
    }
}