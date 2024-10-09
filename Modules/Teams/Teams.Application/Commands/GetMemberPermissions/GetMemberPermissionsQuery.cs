using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;

namespace Teams.Application.Commands.GetMemberRoles;

public record GetMemberPermissionsQuery(int MemberId): IRequest<Result<List<MemberPermissionDto>>>;