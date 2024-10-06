using MediatR;

namespace Application.Commands.Logout;

public record LogoutCommand(string sessionId) : IRequest<Unit>;
