using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Validation;

public class CreateSaleCommandValidatorTests
{
    private readonly CreateSaleCommandValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_For_ValidData()
    {
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente Teste",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial Teste",
            Items = new List<SaleItemCommand>
            {
                new SaleItemCommand
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto Teste",
                    Quantity = 2,
                    UnitPrice = 10
                }
            }
        };

        var result = _validator.TestValidate(command);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Fail_When_Items_Is_Empty()
    {
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente Teste",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial Teste",
            Items = new List<SaleItemCommand>()
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    [Fact]
    public void Should_Fail_When_CustomerName_TooLong()
    {
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = new string('A', 151),
            BranchId = Guid.NewGuid(),
            BranchName = "Filial Teste",
            Items = new List<SaleItemCommand>
            {
                new SaleItemCommand
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto Teste",
                    Quantity = 2,
                    UnitPrice = 10
                }
            }
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    [Fact]
    public void Should_Fail_When_Item_Quantity_Is_Zero()
    {
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Cliente Teste",
            BranchId = Guid.NewGuid(),
            BranchName = "Filial Teste",
            Items = new List<SaleItemCommand>
            {
                new SaleItemCommand
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto Teste",
                    Quantity = 0,
                    UnitPrice = 10
                }
            }
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveAnyValidationError();
    }
}