using FluentResults;
using MediatR;

namespace Teams.Application.Commands.DeleteInvitation;

public record DeleteInvitationCommand(int ProjectId, int InvitationId): IRequest<Result<Unit>>;