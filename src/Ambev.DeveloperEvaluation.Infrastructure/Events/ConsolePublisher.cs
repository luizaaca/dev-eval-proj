using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        var eventJson = JsonSerializer.Serialize(newEvent, newEvent.GetType(), new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        });
        _logger.LogInformation("Publishing event: {EventName}, Data: {EventData}", eventName, eventJson);
        return Task.CompletedTask;
    }
}