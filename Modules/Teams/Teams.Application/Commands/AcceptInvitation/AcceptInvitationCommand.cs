using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;

namespace Teams.Application.Commands.AcceptInvitation;

public record AcceptInvitationCommand(AcceptInvitationDto dto, int projectId, int memberId) : IRequest<Result<Unit>>;