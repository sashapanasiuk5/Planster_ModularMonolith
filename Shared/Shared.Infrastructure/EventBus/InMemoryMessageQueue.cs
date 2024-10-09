using System.Threading.Channels;
using Infrastructure.EventBus;
using Shared.Contracts.EventBus;

namespace Shared.Infrastructure.EventBus;

public class InMemoryMessageQueue
{
    private readonly Channel<IIntegrationEvent> _channel = Channel.CreateUnbounded<IIntegrationEvent>();

    public ChannelWriter<IIntegrationEvent> Writer => _channel.Writer;
    public ChannelReader<IIntegrationEvent> Reader => _channel.Reader;
}