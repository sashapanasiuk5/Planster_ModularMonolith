using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Invitation;

namespace Teams.Application.Commands.GetInvitationByCode;

public record GetInvitationByCodeQuery(int ProjectId, string Code): IRequest<Result<InvitationInfoDto>>;