using Application.Interfaces;
using MediatR;

namespace Application.Commands.Logout;

public class LogoutCommandHandler: IRequestHandler<LogoutCommand, Unit>
{
    private readonly ISessionStore _sessionStore;

    public LogoutCommandHandler(ISessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _sessionStore.EndSessionAsync(request.sessionId);
        return Unit.Value;
    }
}