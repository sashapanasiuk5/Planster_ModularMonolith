using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared.Infrastructure.EventBus;

public class IntegrationEventProcessorJob: BackgroundService
{
    private readonly InMemoryMessageQueue _messageQueue;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<IntegrationEventProcessorJob> _logger;

    public IntegrationEventProcessorJob(IServiceScopeFactory scopeFactory, InMemoryMessageQueue messageQueue, ILogger<IntegrationEventProcessorJob> logger)
    {
        _messageQueue = messageQueue;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var integrationEvent = await _messageQueue.Reader.ReadAsync(stoppingToken);
                _logger.LogInformation($"Processing integration event: {integrationEvent.Id}");
                using var scope = _scopeFactory.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                await publisher.Publish(integrationEvent, stoppingToken);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred executing task work item."); 
            }
        }
    }
}