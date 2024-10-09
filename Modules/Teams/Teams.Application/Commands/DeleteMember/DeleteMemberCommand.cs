using FluentResults;
using MediatR;

namespace Teams.Application.Commands.DeleteMember;

public record DeleteMemberCommand(int MemberId, int ProjectId): IRequest<Result<Unit>>;