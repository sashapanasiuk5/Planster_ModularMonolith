using Application.Interfaces;
using Application.Mappers;
using Domain.Models;
using FluentResults;
using Identity.Contracts.Dtos;
using Infrastructure.Utils;
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
    public async Task<Session?> GetById(string sessionId)
    {
        var sessionJson = await _cache.GetStringAsync(sessionId);
        if (string.IsNullOrEmpty(sessionJson))
        {
            return null;
        }
        var session = JsonConvert.DeserializeObject<SessionDto>(sessionJson, new JsonSerializerSettings()
        {
            ContractResolver = new PrivateResolver()
        });
        if(session == null)
            throw new Exception("Cannot convert session json to Session");
        return session.ToSession();
    }

    public async Task<string?> GetRawById(string sessionId)
    {
        var sessionJson = await _cache.GetStringAsync(sessionId);
        if (string.IsNullOrEmpty(sessionJson))
        {
            return null;
        }

        return sessionJson;
    }

    public async Task EndSessionAsync(string sessionId)
    {
        await _cache.RemoveAsync(sessionId);
    }

    public async Task UpdateSessionAsync(Session session)
    {
        await _cache.RemoveAsync(session.Id.ToString());
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = Session.SessionDuration
        };
        var payload = JsonConvert.SerializeObject(session.ToDto());
        await _cache.SetStringAsync(session.Id.ToString(), payload, options);
    }

    public async Task StartSessionAsync(Session session)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = Session.SessionDuration
        };
        var payload = JsonConvert.SerializeObject(session.ToDto());
        await _cache.SetStringAsync(session.Id.ToString(), payload, options);
    }
}