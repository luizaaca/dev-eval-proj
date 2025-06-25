using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;
public interface IEventPublisher
{
    Task PublishEventAsync(INotification newEvent, CancellationToken cancellationToken);
}