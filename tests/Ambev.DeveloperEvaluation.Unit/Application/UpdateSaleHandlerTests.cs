using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ISaleCanBeUpdatedSpecification _saleCanBeUpdatedSpecification = Substitute.For<ISaleCanBeUpdatedSpecification>();
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _handler = new UpdateSaleHandler(_saleRepository, _mediator, _saleCanBeUpdatedSpecification);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
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
                new UpdateSaleItemDto { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 10.0m, ProductName = "Product A" },
                new UpdateSaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 20.0m, ProductName = "Product B" }

            }
        };
        
        var sale = new Sale { 
            Id = command.Id, 
            CustomerId = command.CustomerId, 
            CustomerName = command.CustomerName,
            Date = command.Date,
            BranchId = command.BranchId,
            BranchName = command.BranchName,
            Status = DeveloperEvaluation.Domain.Enums.SaleStatus.Active,
            Items = new List<SaleItem>() };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleRepository.UpdateAsync(sale, Arg.Any<CancellationToken>()).Returns(true);
        _saleCanBeUpdatedSpecification.IsSatisfiedBy(sale).Returns(true);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_SaleNotFound_ReturnsFail()
    {
        var command = new UpdateSaleCommand { Id = Guid.NewGuid() };
        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Success.Should().BeFalse();
        response.Message.Should().Contain("Sale not found");
    }

    [Fact(DisplayName = "Handle should deny update if specification returns false")]
    public async Task Handle_SpecificationNotSatisfied_ReturnsFail()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = []
        };
        var sale = new Sale
        {
            Id = command.Id,
            CustomerId = command.CustomerId,
            BranchId = command.BranchId,
            Items = []
        };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleCanBeUpdatedSpecification.IsSatisfiedBy(sale).Returns(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Message.Should().Contain("Sale cannot be updated.");
    }

}