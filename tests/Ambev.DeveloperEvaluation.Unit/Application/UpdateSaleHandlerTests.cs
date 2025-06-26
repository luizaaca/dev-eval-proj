using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
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
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _handler = new UpdateSaleHandler(_saleRepository, _mediator);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccess()
    {
        var command = new UpdateSaleCommand { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), BranchId = Guid.NewGuid(), Items = new() };
        var sale = new Sale { Id = command.Id, CustomerId = command.CustomerId, BranchId = command.BranchId, Items = new List<SaleItem>() };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _saleRepository.UpdateAsync(sale, Arg.Any<CancellationToken>()).Returns(true);

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
        response.Message.Should().Contain("Venda não encontrada");
    }
}