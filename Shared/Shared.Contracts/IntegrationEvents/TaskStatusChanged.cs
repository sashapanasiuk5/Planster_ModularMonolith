using Shared.Contracts.EventBus;

namespace Shared.Contracts.IntegrationEvents;

public record TaskStatusChanged(Guid EventId, int TaskId, int StatusId): IntegrationEvent(EventId);