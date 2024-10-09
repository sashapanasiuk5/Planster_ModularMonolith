using Domain.Models;
using Identity.Contracts.Dtos;

namespace Application.Interfaces;

public interface ISessionStore
{
    Task<Session?> GetById(string sessionId);
    Task<string?> GetRawById(string sessionId);
    Task EndSessionAsync(string sessionId);
    Task UpdateSessionAsync(Session session);
    Task StartSessionAsync(Session session);
}