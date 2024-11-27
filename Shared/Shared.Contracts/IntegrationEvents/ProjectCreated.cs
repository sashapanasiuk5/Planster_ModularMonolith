using Shared.Contracts.Dto.Teams;
using Shared.Contracts.EventBus;

namespace Shared.Contracts.IntegrationEvents;

public record ProjectCreated(Guid EventId, ProjectDto Project): IntegrationEvent(EventId);