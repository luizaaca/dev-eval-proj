using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Testes unitários para a entidade Sale.
/// </summary>
public class SaleTests
{
    [Fact(DisplayName = "TotalAmount deve somar corretamente os itens")]
    public void Given_SaleWithItems_When_CalculatingTotalAmount_Then_ShouldReturnSum()
    {
        // Arrange
        var sale = new Sale
        {
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 2, UnitPrice = 10m },
                new SaleItem { Quantity = 1, UnitPrice = 5m }
            }
        };

        // Act
        var total = sale.TotalAmount;

        // Assert
        total.Should().Be(2 * 10m + 1 * 5m);
    }   

    [Fact(DisplayName = "Sale deve inicializar Items como lista vazia")]
    public void Sale_Should_Initialize_Items_As_EmptyList()
    {
        // Arrange & Act
        var sale = new Sale();

        // Assert
        sale.Items.Should().NotBeNull();
        sale.Items.Should().BeEmpty();
    }
}