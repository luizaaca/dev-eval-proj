using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class GetSaleByIdQueryValidatorTests
{
    private readonly GetSaleByIdQueryValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_For_ValidId()
    {
        var query = new GetSaleByIdQuery(Guid.NewGuid());
        var result = _validator.TestValidate(query);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Fail_For_EmptyId()
    {
        var query = new GetSaleByIdQuery(Guid.Empty);
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}