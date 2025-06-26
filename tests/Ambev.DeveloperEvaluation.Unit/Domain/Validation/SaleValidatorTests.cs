using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleValidatorTests
{
    private readonly SaleValidator _validator = new();

    [Fact(DisplayName = "Valid Sale should pass validation")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        var sale = new Sale
        {
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto",
                    Quantity = 5,
                    UnitPrice = 10m
                }
            }
        };

        var result = _validator.TestValidate(sale);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Sale with no items should fail validation")]
    public void Given_SaleWithNoItems_When_Validated_Then_ShouldHaveError()
    {
        var sale = new Sale
        {
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            Items = new List<SaleItem>()
        };

        var result = _validator.TestValidate(sale);

        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    [Fact(DisplayName = "Sale with null items should fail validation")]
    public void Given_SaleWithNullItems_When_Validated_Then_ShouldHaveError()
    {
        var sale = new Sale
        {
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            Items = null
        };

        var result = _validator.TestValidate(sale);

        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    [Fact(DisplayName = "Sale with invalid item should fail validation")]
    public void Given_SaleWithInvalidItem_When_Validated_Then_ShouldHaveError()
    {
        var sale = new Sale
        {
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            SaleDate = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Produto",
                    Quantity = 0,
                    UnitPrice = 10m
                }
            }
        };

        var result = _validator.TestValidate(sale);

        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }
}