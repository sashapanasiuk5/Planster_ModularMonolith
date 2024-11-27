using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;

namespace Teams.Application.Commands.CreateInvitation;

public record CreateInvitationCommand(CreateUpdateInvitationDto Dto, int ProjectId) :IRequest<Result<InvitationDto>>;