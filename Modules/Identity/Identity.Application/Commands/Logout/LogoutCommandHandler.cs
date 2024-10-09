using Application.Interfaces;
using FluentResults;
using MediatR;

namespace Application.Commands.Logout;

public class LogoutCommandHandler: IRequestHandler<LogoutCommand, Result<Unit>>
{
    private readonly ISessionStore _sessionStore;
    private readonly IIdentityRepository _identityRepository;

    public LogoutCommandHandler(ISessionStore sessionStore, IIdentityRepository identityRepository)
    {
        _sessionStore = sessionStore;
        _identityRepository = identityRepository;
    }
    public async Task<Result<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var identity = await _identityRepository.GetByIdAsync(request.UserId);
        if (identity == null)
            return Result.Fail("User does not exist");
        await _sessionStore.EndSessionAsync(request.SessionId);
        return Unit.Value;
    }
}