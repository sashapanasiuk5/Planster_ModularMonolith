using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;

namespace Teams.Application.Commands.GetInvitations;

public record GetInvitationsQuery(int ProjectId): IRequest<Result<List<InvitationDto>>>;