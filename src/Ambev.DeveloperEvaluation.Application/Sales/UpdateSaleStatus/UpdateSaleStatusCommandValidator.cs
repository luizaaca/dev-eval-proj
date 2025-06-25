using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleStatus;

public class UpdateSaleStatusCommandValidator : AbstractValidator<UpdateSaleStatusCommand>
{
    public UpdateSaleStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID da venda não pode ser vazio.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status da venda inválido.");
    }
}