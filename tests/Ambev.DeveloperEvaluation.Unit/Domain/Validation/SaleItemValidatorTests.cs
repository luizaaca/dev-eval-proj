using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator = new();

    [Fact(DisplayName = "Valid SaleItem should pass validation")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Produto",
            Quantity = 5,
            UnitPrice = 10m
        };

        var result = _validator.TestValidate(item);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Quantity above 20 should fail validation")]
    public void Given_QuantityAbove20_When_Validated_Then_ShouldHaveError()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Produto",
            Quantity = 21,
            UnitPrice = 10m
        };

        var result = _validator.TestValidate(item);

        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact(DisplayName = "Quantity below 1 should fail validation")]
    public void Given_QuantityBelow1_When_Validated_Then_ShouldHaveError()
    {
        var item = new SaleItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Produto",
            Quantity = 0,
            UnitPrice = 10m
        };

        var result = _validator.TestValidate(item);

        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }
}