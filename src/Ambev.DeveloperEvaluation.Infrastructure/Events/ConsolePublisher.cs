using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Infrastructure.Events;
public class ConsolePublisher : IEventPublisher
{
    private readonly ILogger<ConsolePublisher> _logger;

    public ConsolePublisher(ILogger<ConsolePublisher> logger)
    {
        _logger = logger;
    }

    public Task PublishEventAsync(INotification newEvent, CancellationToken cancellationToken)
    {
        var eventName = newEvent.GetType().Name;
        var eventJson = System.Text.Json.JsonSerializer.Serialize(newEvent);
        _logger.LogInformation("Publishing event: {EventName}, Data: {EventData}", eventName, eventJson);
        return Task.CompletedTask;
    }
}