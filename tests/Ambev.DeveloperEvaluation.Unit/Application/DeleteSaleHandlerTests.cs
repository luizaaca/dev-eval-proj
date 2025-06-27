using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ISaleCanBeDeletedSpecification _saleCanBeDeletedSpecification = Substitute.For<ISaleCanBeDeletedSpecification>();
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _handler = new DeleteSaleHandler(_saleRepository, _mediator, _saleCanBeDeletedSpecification);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        var command = new DeleteSaleCommand(Guid.NewGuid());
        var sale = new Sale { Id = command.Id, Status = DeveloperEvaluation.Domain.Enums.SaleStatus.Cancelled };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleRepository.DeleteAsync(sale.Id, Arg.Any<CancellationToken>()).Returns(true);
        _saleCanBeDeletedSpecification.IsSatisfiedBy(sale).Returns(true);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_SaleNotFound_ReturnsFail()
    {
        var command = new DeleteSaleCommand(Guid.NewGuid());
        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Sale?)null);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Success.Should().BeFalse();
        response.Message.Should().Contain("Sale not found");
    }

    [Fact(DisplayName = "Handle should deny deletion if specification returns false")]
    public async Task Handle_SpecificationDeniesDeletion_ReturnsFail()
    {
        var command = new DeleteSaleCommand(Guid.NewGuid());
        var sale = new Sale { Id = command.Id, Status = DeveloperEvaluation.Domain.Enums.SaleStatus.Active };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleCanBeDeletedSpecification.IsSatisfiedBy(sale).Returns(false);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Success.Should().BeFalse();
        response.Message.Should().Contain("Sale cannot be deleted.");
    }

    [Fact(DisplayName = "Handle should return fail if repository deletion fails")]
    public async Task Handle_RepositoryDeleteFails_ReturnsFail()
    {
        var command = new DeleteSaleCommand(Guid.NewGuid());
        var sale = new Sale { Id = command.Id, Status = DeveloperEvaluation.Domain.Enums.SaleStatus.Cancelled };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleCanBeDeletedSpecification.IsSatisfiedBy(sale).Returns(true);
        _saleRepository.DeleteAsync(sale.Id, Arg.Any<CancellationToken>()).Returns(false);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Success.Should().BeFalse();
        response.Message.Should().Contain("Failed to delete");
    }

    [Fact(DisplayName = "Handle should call specification to check delete permission")]
    public async Task Handle_ShouldCallSpecificationToCheckDeletePermission()
    {
        var command = new DeleteSaleCommand(Guid.NewGuid());
        var sale = new Sale { Id = command.Id, Status = DeveloperEvaluation.Domain.Enums.SaleStatus.Cancelled };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleCanBeDeletedSpecification.IsSatisfiedBy(sale).Returns(true);
        _saleRepository.DeleteAsync(sale.Id, Arg.Any<CancellationToken>()).Returns(true);

        await _handler.Handle(command, CancellationToken.None);

        _saleCanBeDeletedSpecification.Received(1).IsSatisfiedBy(sale);
    }
}