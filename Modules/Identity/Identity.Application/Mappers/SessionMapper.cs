using Domain.Models;
using Identity.Contracts.Dtos;

namespace Application.Mappers;

public static class SessionMapper
{
    public static SessionDto ToDto(this Session session)
    {
        return new SessionDto()
        {
            Id = session.Id.ToString(),
            IdentityId = session.IdentityId,
            Permissions = session.Permissions.ToList(),
        };
    }

    public static Session ToSession(this SessionDto sessionDto)
    {
        return new Session(sessionDto.Id, sessionDto.IdentityId, sessionDto.ExpirationDateTime, sessionDto.Permissions.ToList());
    }
}