using Infrastructure.Dto;

namespace Infrastructure.ModulesInterfaces;

public interface IIdentityModule
{
    Task<SessionDto> AddNewIdentityAsync(NewUserDto user);
    Task<int?> GetIdentityAsync(string sessionId);
}