using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Theory(DisplayName = "Discount deve ser 0 para quantidade menor que 4")]
    [InlineData(1, 10)]
    [InlineData(3, 5)]
    public void Discount_ShouldBeZero_When_QuantityIsLessThanFour(int quantity, decimal unitPrice)
    {
        var item = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = unitPrice
        };

        item.Discount.Should().Be(0);
    }

    [Theory(DisplayName = "Discount deve ser 10% para quantidade entre 4 e 9")]
    [InlineData(4, 10)]
    [InlineData(9, 5)]
    public void Discount_ShouldBeTenPercent_When_QuantityBetweenFourAndNine(int quantity, decimal unitPrice)
    {
        var item = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = unitPrice
        };

        var expectedDiscount = quantity * unitPrice * 0.10m;
        item.Discount.Should().Be(expectedDiscount);
    }

    [Theory(DisplayName = "Discount deve ser 20% para quantidade entre 10 e 20")]
    [InlineData(10, 10)]
    [InlineData(20, 5)]
    public void Discount_ShouldBeTwentyPercent_When_QuantityBetweenTenAndTwenty(int quantity, decimal unitPrice)
    {
        var item = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = unitPrice
        };

        var expectedDiscount = quantity * unitPrice * 0.20m;
        item.Discount.Should().Be(expectedDiscount);
    }

    [Fact(DisplayName = "TotalAmount deve considerar desconto corretamente")]
    public void TotalAmount_ShouldBe_Correct()
    {
        var item = new SaleItem
        {
            Quantity = 10,
            UnitPrice = 10
        };

        var expectedDiscount = 10 * 10 * 0.20m;
        var expectedTotal = 10 * 10 - expectedDiscount;

        item.TotalAmount.Should().Be(expectedTotal);
    }

    [Fact(DisplayName = "Propriedades básicas devem ser atribuídas corretamente")]
    public void Properties_ShouldBe_SetCorrectly()
    {
        var id = Guid.NewGuid();
        var saleId = Guid.NewGuid();
        var item = new SaleItem
        {
            Id = id,
            ProductId = Guid.NewGuid(),
            ProductName = "Produto Teste",
            Quantity = 2,
            UnitPrice = 15,
            SaleId = saleId
        };

        item.Id.Should().Be(id);
        item.ProductName.Should().Be("Produto Teste");
        item.Quantity.Should().Be(2);
        item.UnitPrice.Should().Be(15);
        item.SaleId.Should().Be(saleId);
    }
}