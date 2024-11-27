using Shared.Contracts.EventBus;

namespace Shared.Contracts.IntegrationEvents;

public record NewTeamMemberInvited(Guid EventId,
                                   int MemberId,
                                   int ProjectId,
                                   string Name,
                                   string Email): IntegrationEvent(EventId);