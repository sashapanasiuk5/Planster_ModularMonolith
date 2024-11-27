using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;

namespace Teams.Application.Commands.UpdateInvitation;

public record UpdateInvitationCommand(int ProjectId, int Id, CreateUpdateInvitationDto Invitation): IRequest<Result<Unit>>;