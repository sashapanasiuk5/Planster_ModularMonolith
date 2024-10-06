using Infrastructure.Dto;

namespace Application.Interfaces;

public interface ISessionStore
{
    Task<SessionDto?> GetById(string sessionId);
    Task EndSessionAsync(string sessionId);
    Task<SessionDto> StartSessionAsync(int userId);
}