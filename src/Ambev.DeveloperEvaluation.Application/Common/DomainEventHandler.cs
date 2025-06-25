using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

public class DomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification
{
    private readonly ILogger<DomainEventHandler<TEvent>> _logger;
    private readonly IEventPublisher _eventPublisher;

    public DomainEventHandler(ILogger<DomainEventHandler<TEvent>> logger, IEventPublisher eventPublisher)
    {
        _logger = logger;
        _eventPublisher = eventPublisher;
    }

    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        
        await _eventPublisher.PublishEventAsync(notification, cancellationToken);
    }
}