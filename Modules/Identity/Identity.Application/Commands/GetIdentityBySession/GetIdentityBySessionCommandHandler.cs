using Application.Interfaces;
using Domain.Models;
using FluentResults;
using MediatR;

namespace Application.Commands.GetIdentityBySession;

public class GetIdentityBySessionCommandHandler: IRequestHandler<GetIdentityBySessionCommand, Result<int>>
{
    private readonly ISessionStore _sessionStore;

    public GetIdentityBySessionCommandHandler(ISessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }
    public async Task<Result<int>> Handle(GetIdentityBySessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionStore.GetById(request.sessionId);
        if (session != null)
        {
            return Result.Ok(session.UserId);
        }
        return Result.Fail<int>("Session not found");
    }
}