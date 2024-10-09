using Shared.Contracts.EventBus;

namespace Infrastructure.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T integrationEvent, CancellationToken token = default)
            where T : class, IIntegrationEvent;
    }
}