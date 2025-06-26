using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class SaleCanBeDeletedSpecificationTests
{
    [Theory]
    [InlineData(SaleStatus.Cancelled, true)]
    [InlineData(SaleStatus.Active, false)]
    public void IsSatisfiedBy_ShouldValidateSaleStatus(SaleStatus status, bool expectedResult)
    {
        // Arrange
        var sale = new Sale { Status = status };
        var specification = new SaleCanBeDeletedSpecification();

        // Act
        var result = specification.IsSatisfiedBy(sale);

        // Assert
        result.Should().Be(expectedResult);
    }
}