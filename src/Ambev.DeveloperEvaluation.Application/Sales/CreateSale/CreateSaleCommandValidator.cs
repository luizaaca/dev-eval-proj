using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Sale date is required.")
            .GreaterThan(DateTime.MinValue).WithMessage("Sale date must be greater than the minimum value.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(150).WithMessage("Customer name cannot exceed 150 characters.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("Branch ID is required.");

        RuleFor(x => x.BranchName)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(150).WithMessage("Branch name cannot exceed 150 characters.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.")
            .ForEach(item => item.SetValidator(new SaleItemCommandValidator()));
    }
}

public class SaleItemCommandValidator : AbstractValidator<SaleItemCommand>
{
    public SaleItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required for each item.");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required for each item.")
            .MaximumLength(150).WithMessage("Product name cannot exceed 150 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("It's not possible to sell above 20 identical items.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
    }
}