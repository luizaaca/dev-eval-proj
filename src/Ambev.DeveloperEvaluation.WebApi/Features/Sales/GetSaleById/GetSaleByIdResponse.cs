using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;

public record GetSaleByIdResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public required string CustomerName { get; init; }
    public DateTime Date { get; init; }
    public Guid BranchId { get; init; }
    public required string BranchName { get; init; }
    public required string Status { get; init; }
    public decimal TotalAmount { get; init; }
    public required IEnumerable<GetSaleByIdItemResponse> Items { get; init; }

    public GetSaleByIdResponse() { }

    public GetSaleByIdResponse(
        Guid id,
        Guid customerId,
        string customerName,
        DateTime date,
        Guid branchId,
        string branchName,
        string status,
        decimal totalAmount,
        IEnumerable<GetSaleByIdItemResponse> items)
    {
        Id = id;
        CustomerId = customerId;
        CustomerName = customerName;
        Date = date;
        BranchId = branchId;
        BranchName = branchName;
        Status = status;
        TotalAmount = totalAmount;
        Items = items;
    }
}

public record GetSaleByIdItemResponse
{
    public Guid ProductId { get; init; }
    public required string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Discount { get; init; }
    public decimal TotalAmount { get; init; }

    public GetSaleByIdItemResponse() { }

    public GetSaleByIdItemResponse(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        TotalAmount = totalAmount;
    }
}