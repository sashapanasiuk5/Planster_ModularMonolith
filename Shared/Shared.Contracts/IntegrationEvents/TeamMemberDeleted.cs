using Shared.Contracts.EventBus;

namespace Shared.Contracts.IntegrationEvents;

public record TeamMemberDeleted(Guid EventId, int MemberId, int ProjectId): IntegrationEvent(EventId);