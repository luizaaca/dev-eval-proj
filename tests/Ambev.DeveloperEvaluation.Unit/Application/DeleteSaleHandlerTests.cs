using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _handler = new DeleteSaleHandler(_saleRepository, _mediator);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        var command = new DeleteSaleCommand(Guid.NewGuid());
        var sale = new Sale { Id = command.Id, Status = DeveloperEvaluation.Domain.Enums.SaleStatus.Cancelled };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleRepository.DeleteAsync(sale.Id, Arg.Any<CancellationToken>()).Returns(true);

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
        response.Message.Should().Contain("Venda não encontrada");
    }
}