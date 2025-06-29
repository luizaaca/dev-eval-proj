using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("BranchId is required.");

        RuleFor(x => x.BranchName).NotEmpty()
            .WithMessage("BranchName is required.");

        RuleFor(x => x.Status).NotEmpty()
            .WithMessage("Status is required.")
            .Must(status => Enum.TryParse(status, out SaleStatus _))
            .WithMessage("Invalid status value.");

        RuleFor(x => x.Items)
            .NotNull()
            .WithMessage("Items are required.")
            .Must((command, items) => items == null || items.Count > 0)
            .WithMessage("At least one item is required.");

        RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemDtoValidator());
    }
}

public class UpdateSaleItemDtoValidator : AbstractValidator<UpdateSaleItemDto>
{
    public UpdateSaleItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20).WithMessage("Maximum allowed quantity per product is 20.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("ProductName is required.");
    }
}