using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Maximum limit is 20 items per product.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");        

        RuleFor(item => item.ProductName).NotNull().WithMessage("Product name cannot be null.")
            .NotEmpty().WithMessage("Product name cannot be empty.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(item => item.ProductId).NotEmpty().WithMessage("Product ID cannot be empty.")
            .NotNull().WithMessage("Product ID cannot be null.");
    }
}