using Application.Interfaces;
using FluentResults;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class SessionStore: ISessionStore
{
    private readonly IDistributedCache _cache;

    public SessionStore(IDistributedCache cache)
    {
        _cache = cache;
    }
    public async Task<SessionDto?> GetById(string sessionId)
    {
        var sessionJson = await _cache.GetStringAsync(sessionId);
        if (string.IsNullOrEmpty(sessionJson))
        {
            return null;
        }
        var userId = Int32.Parse(sessionJson);
        return new SessionDto() { SessionId = sessionId, UserId = userId };
    }

    public async Task EndSessionAsync(string sessionId)
    {
        await _cache.RemoveAsync(sessionId);
    }

    public async Task<SessionDto> StartSessionAsync(int userId)
    {
        var sessionId = Guid.NewGuid().ToString();
        await _cache.SetStringAsync(sessionId, userId.ToString());
        return new SessionDto()
        {
            SessionId = sessionId,
            UserId = userId
        };
    }
}