using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _mediator = Substitute.For<IMediator>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _mediator);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Customer Name",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch Name",
            Items = new List<SaleItemCommand>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Product", Quantity = 2, UnitPrice = 10 }
            }
        };
        var sale = new Sale()
        {
            Id = Guid.NewGuid(),
            Date = command.Date,
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            BranchId = command.BranchId,
            BranchName = command.BranchName,
            Items = new List<SaleItem>()
            {
                new SaleItem
                {
                    ProductId = command.Items[0].ProductId,
                    ProductName = command.Items[0].ProductName,
                    Quantity = command.Items[0].Quantity,
                    UnitPrice = command.Items[0].UnitPrice
                }
            }
        };
        var result = new CreateSaleResult(
            sale.Id, 
            DateTime.UtcNow, 
            command.CustomerId, 
            "Customer Name", 
            command.BranchId,
            "Branch Name", 
            new List<SaleItemResult>(), 
            20m, 
            SaleStatus.Active.ToString()
        );

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>()).Returns(true);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        response.Data.Should().Be(result);
        await _mediator.Received(1).Publish(Arg.Is<SaleCreatedEvent>(e => e.Sale.Id == sale.Id), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given repository fails When creating sale Then returns fail response")]
    public async Task Handle_RepositoryFails_ReturnsFailResponse()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Customer Name",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch Name",
            Items = new List<SaleItemCommand>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Product", Quantity = 2, UnitPrice = 10 }
            }
        };
        var sale = new Sale()
        {
            Id = Guid.NewGuid(),
            Date = command.Date,
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            BranchId = command.BranchId,
            BranchName = command.BranchName,
            Items = new List<SaleItem>()
            {
                new SaleItem
                {
                    ProductId = command.Items[0].ProductId,
                    ProductName = command.Items[0].ProductName,
                    Quantity = command.Items[0].Quantity,
                    UnitPrice = command.Items[0].UnitPrice
                }
            }
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>()).Returns(false);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Message.Should().Be("Failed to create the sale.");
    }

    [Fact(DisplayName = "Given empty items When creating sale Then returns fail response")]
    public async Task Handle_EmptyItems_ReturnsFailResponse()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<SaleItemCommand>()
        };

        _mapper.Map<Sale>(command).Returns((Sale)null);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
    }

    [Fact(DisplayName = "Given null sale from mapper When creating sale Then returns fail response")]
    public async Task Handle_NullSaleFromMapper_ReturnsFailResponse()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<SaleItemCommand>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Product", Quantity = 2, UnitPrice = 10 }
            }
        };

        _mapper.Map<Sale>(command).Returns((Sale)null);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
    }

    [Fact(DisplayName = "Given repository throws exception When creating sale Then returns fail response")]
    public async Task Handle_RepositoryThrowsException_ReturnsFailResponse()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Date = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Test Customer",
            BranchId = Guid.NewGuid(),
            BranchName = "Test Branch",
            Items = new List<SaleItemCommand>
            {
                new() { ProductId = Guid.NewGuid(), ProductName = "Product", Quantity = 2, UnitPrice = 10 }
            }
        };
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Date = command.Date,
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            BranchId = command.BranchId,
            BranchName = command.BranchName,
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    ProductId = command.Items[0].ProductId,
                    ProductName = command.Items[0].ProductName,
                    Quantity = command.Items[0].Quantity,
                    UnitPrice = command.Items[0].UnitPrice
                }
            }
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>()).Throws(new Exception("Unexpected error"));

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Message.Should().Contain("Unexpected error");
    }

    //[Fact(DisplayName = "Given zero total amount When creating sale Then returns fail response")]
    //public async Task Handle_ZeroTotalAmount_ReturnsFailResponse()
    //{
    //    // Arrange
    //    var command = new CreateSaleCommand
    //    {
    //        SaleDate = DateTime.UtcNow,
    //        CustomerId = Guid.NewGuid(),
    //        BranchId = Guid.NewGuid(),
    //        Items = new List<SaleItemCommand>
    //        {
    //            new() { ProductId = Guid.NewGuid(), ProductName = "Produto", Quantity = 0, UnitPrice = 0 }
    //        }
    //    };
    //    var sale = new Sale
    //    {
    //        Id = Guid.NewGuid(),
    //        SaleDate = command.SaleDate,
    //        CustomerId = command.CustomerId,
    //        BranchId = command.BranchId,
    //        Items = new List<SaleItem>
    //        {
    //            new SaleItem
    //            {
    //                ProductId = command.Items[0].ProductId,
    //                ProductName = command.Items[0].ProductName,
    //                Quantity = 0,
    //                UnitPrice = 0
    //            }
    //        }
    //    };

    //    _mapper.Map<Sale>(command).Returns(sale);
    //    _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>()).Returns(true);

    //    // Act
    //    var response = await _handler.Handle(command, CancellationToken.None);

    //    // Assert
    //    response.Success.Should().BeFalse();
    //}

    // [Fact(DisplayName = "Given future sale date When creating sale Then returns fail response")]
    // public async Task Handle_FutureSaleDate_ReturnsFailResponse()
    // {
    //     // Arrange
    //     var command = new CreateSaleCommand
    //     {
    //         SaleDate = DateTime.UtcNow.AddDays(1),
    //         CustomerId = Guid.NewGuid(),
    //         BranchId = Guid.NewGuid(),
    //         Items = new List<SaleItemCommand>
    //         {
    //             new() { ProductId = Guid.NewGuid(), ProductName = "Produto", Quantity = 2, UnitPrice = 10 }
    //         }
    //     };

    //     _mapper.Map<Sale>(command).Returns((Sale)null);

    //     // Act
    //     var response = await _handler.Handle(command, CancellationToken.None);

    //     // Assert
    //     response.Success.Should().BeFalse();
    // }
}