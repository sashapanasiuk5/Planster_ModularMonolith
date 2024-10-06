using Infrastructure.Dto;
using Shared.Infrastructure.EventBus;

namespace Infrastructure.IntegrationEvents;

public record UserRegistered(Guid Id,  NewUserDto Dto) : IntegrationEvent(Id);
