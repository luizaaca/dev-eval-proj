using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleDeletedEvent(Guid SaleId) : INotification;