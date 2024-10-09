using MediatR;

namespace Shared.Contracts.EventBus;

public interface IIntegrationEvent: INotification
{
    Guid EventId { get; init; }
}

public abstract record IntegrationEvent(Guid EventId) : IIntegrationEvent;
