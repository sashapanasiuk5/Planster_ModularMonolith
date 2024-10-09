using Shared.Contracts.EventBus;
using Shared.Infrastructure.EventBus;

namespace Infrastructure.EventBus;

public class EventBus: IEventBus
{
    private readonly InMemoryMessageQueue _messageQueue;

    public EventBus(InMemoryMessageQueue messageQueue)
    {
        _messageQueue = messageQueue;
    }
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken token = default) where T : class, IIntegrationEvent
    {
        await _messageQueue.Writer.WriteAsync(integrationEvent, token);
    }
}