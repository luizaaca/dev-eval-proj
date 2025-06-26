using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("BranchId is required.");

        //RuleFor(sale => sale.SaleDate)
        //    .NotEmpty().WithMessage("SaleDate is required.");

        RuleFor(sale => sale.Items)
            .NotNull()
            .WithMessage("Items are required.")
            .Must(items => items == null || items.Count > 0)
            .WithMessage("At least one item is required.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}