using Identity.Contracts.Dtos;
using Shared.Contracts.Dto.Teams.Member;
using Users.Contracts.Dto;

namespace Shared.Contracts.ModulesInterfaces;

public interface IIdentityModule
{
    Task<LoginResultDto> AddNewIdentityAsync(NewUserDto user, int userId);
    Task UpdatePermissionsAsync(int userId, List<MemberPermissionDto> permissions);
}