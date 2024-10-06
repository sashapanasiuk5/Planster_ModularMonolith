using MediatR;

namespace Shared.Infrastructure.EventBus;

public interface IIntegrationEvent: INotification
{
    Guid Id { get; init; }
}

public abstract record IntegrationEvent(Guid Id) : IIntegrationEvent;
