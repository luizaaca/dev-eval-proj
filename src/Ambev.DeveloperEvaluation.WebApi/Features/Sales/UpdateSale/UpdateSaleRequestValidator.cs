using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.BranchId).NotEmpty();
        RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.BranchName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Status).NotEmpty()
            .Must(status => Enum.TryParse(status, out SaleStatus _))
            .WithMessage("Invalid status value.");
        RuleFor(x => x.Items).NotNull().Must(i => i != null && i.Any()).WithMessage("At least one item is required.");
        RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemRequestValidator());
    }
}

public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.ProductName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
        RuleFor(x => x.UnitPrice).GreaterThan(0);
    }
}