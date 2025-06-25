using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleStatus;

public class UpdateSaleStatusCommand : IRequest<bool>
{
    [JsonIgnore] // O ID virá da rota, não do corpo da requisição.
    public Guid Id { get; set; }
    public SaleStatus Status { get; set; }
}