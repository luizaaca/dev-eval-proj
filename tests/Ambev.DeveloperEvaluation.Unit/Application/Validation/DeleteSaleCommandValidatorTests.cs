using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Validation;

public class DeleteSaleCommandValidatorTests
{
    private readonly DeleteSaleCommandValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_For_ValidId()
    {
        var command = new DeleteSaleCommand(Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Fail_For_EmptyId()
    {
        var command = new DeleteSaleCommand(Guid.Empty);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}