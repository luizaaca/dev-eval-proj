using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Validation;

public class UpdateSaleCommandValidatorTests
{
    private readonly UpdateSaleCommandValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_For_ValidData()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            CustomerName = "Customer Name",
            Date = DateTime.UtcNow,
            BranchId = Guid.NewGuid(),
            BranchName = "Branch Name",
            Status = "Active",
            Items = new List<UpdateSaleItemDto>
            {
                new UpdateSaleItemDto
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
    public void Should_Fail_When_Items_Is_Null()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            BranchId = Guid.NewGuid(),
            Items = null
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    [Fact]
    public void Should_Fail_When_Item_Quantity_Exceeds_Limit()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            BranchId = Guid.NewGuid(),
            Items = new List<UpdateSaleItemDto>
            {
                new UpdateSaleItemDto
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto Teste",
                    Quantity = 21, //verificar regra
                    UnitPrice = 10,
                    Discount = 0
                }
            }
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveAnyValidationError();
    }
}