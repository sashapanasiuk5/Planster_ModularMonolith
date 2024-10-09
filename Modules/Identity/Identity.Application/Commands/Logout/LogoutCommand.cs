using FluentResults;
using MediatR;

namespace Application.Commands.Logout;

public record LogoutCommand(string SessionId, int UserId) : IRequest<Result<Unit>>;
