using Infrastructure.EventBus;
using Shared.Contracts.EventBus;
using Users.Contracts.Dto;

namespace Shared.Contracts.IntegrationEvents;

public record UserRegistered(Guid EventId,
                             int UserId,
                             string FirstName,
                             string LastName,
                             string Email,
                             string Password,
                             string? ImageUrl) : IntegrationEvent(EventId);
