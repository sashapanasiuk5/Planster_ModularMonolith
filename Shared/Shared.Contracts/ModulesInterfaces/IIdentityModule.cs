using Identity.Contracts.Dtos;
using Shared.Contracts.Dto.Teams.Member;
using Users.Contracts.Dto;

namespace Shared.Contracts.ModulesInterfaces;

public interface IIdentityModule
{
    Task<SessionDto> AddNewIdentityAsync(NewUserDto user, int userId);
    Task<SessionDto?> GetSessionAsync(string sessionId);
    Task UpdatePermissionsAsync(int userId, List<MemberPermissionDto> permissions);
}