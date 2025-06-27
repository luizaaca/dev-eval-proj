using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class SaleCanBeUpdatedSpecificationTests
{
    [Theory]
    [InlineData(SaleStatus.Active, true)]
    [InlineData(SaleStatus.Cancelled, false)]
    public void IsSatisfiedBy_ShouldValidateSaleStatus(SaleStatus status, bool expectedResult)
    {
        // Arrange
        var sale = new Sale { Status = status };
        var specification = new SaleCanBeUpdatedSpecification();

        // Act
        var result = specification.IsSatisfiedBy(sale);

        // Assert
        result.Should().Be(expectedResult);
    }
}