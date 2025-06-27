using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Validation;

public class GetSalesQueryValidatorTests
{
    private readonly GetSalesQueryValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_For_ValidPaging()
    {
        var query = new GetSalesQuery { PageNumber = 1, PageSize = 10 };
        var result = _validator.TestValidate(query);
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, 101)]
    public void Should_Fail_For_InvalidPaging(int pageNumber, int pageSize)
    {
        var query = new GetSalesQuery { PageNumber = pageNumber, PageSize = pageSize };
        var result = _validator.TestValidate(query);
        if (pageNumber <= 0)
            result.ShouldHaveValidationErrorFor(x => x.PageNumber);
        if (pageSize <= 0 || pageSize > 100)
            result.ShouldHaveValidationErrorFor(x => x.PageSize);
    }
}