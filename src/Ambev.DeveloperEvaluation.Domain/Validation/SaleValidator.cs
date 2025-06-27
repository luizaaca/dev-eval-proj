using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("CustomerName is required.")
            .MaximumLength(100).WithMessage("CustomerName cannot exceed 100 characters.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("BranchId is required.");

        RuleFor(sale => sale.BranchName)
            .NotEmpty().WithMessage("BranchName is required.")
            .MaximumLength(100).WithMessage("BranchName cannot exceed 100 characters.");

        RuleFor(sale => sale.Date)
            .NotEmpty().WithMessage("Date is required.")
            .GreaterThan(DateTime.UtcNow.AddDays(-5)).WithMessage("Sale date must be greater than now -5 days.");

        RuleFor(sale => sale.Items)
            .NotNull()
            .WithMessage("Items are required.")
            .Must(items => items == null || items.Count > 0)
            .WithMessage("At least one item is required.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}