using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Common;

public class EventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification
{
    private readonly ILogger<EventHandler<TEvent>> _logger;
    private readonly IEventPublisher _eventPublisher;

    public EventHandler(ILogger<EventHandler<TEvent>> logger, IEventPublisher eventPublisher)
    {
        _logger = logger;
        _eventPublisher = eventPublisher;
    }

    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {

        await _eventPublisher.PublishEventAsync(notification, cancellationToken);
    }
}