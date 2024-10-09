using FluentResults;
using MediatR;
using Shared.Contracts.Dto.Teams.Member;

namespace Application.Commands.UpdatePermissions;

public record UpdatePermissionsCommand(int IdentityId, List<MemberPermissionDto> Permissions): IRequest<Result<Unit>>;