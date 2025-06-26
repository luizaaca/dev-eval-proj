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

    [Fact(DisplayName = "CanBeDeleted deve retornar true apenas se o status for Cancelled")]
    public void Given_SaleStatus_When_CanBeDeleted_Then_ReturnsExpected()
    {
        // Arrange
        var sale = new Sale { Status = SaleStatus.Active };

        // Act & Assert
        sale.CanBeDeleted().Should().BeFalse();

        sale.Status = SaleStatus.Cancelled;
        sale.CanBeDeleted().Should().BeTrue();
    }

    [Fact(DisplayName = "CanBeUpdated deve retornar true apenas se o status for Active")]
    public void Given_SaleStatus_When_CanBeUpdated_Then_ReturnsExpected()
    {
        // Arrange
        var sale = new Sale { Status = SaleStatus.Cancelled };

        // Act & Assert
        sale.CanBeUpdated().Should().BeFalse();

        sale.Status = SaleStatus.Active;
        sale.CanBeUpdated().Should().BeTrue();
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