using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleStatus;

public class UpdateSaleStatusRequestValidator : AbstractValidator<UpdateSaleStatusRequest>
{
    public UpdateSaleStatusRequestValidator()
    {
        RuleFor(x => x.Status).NotEmpty();
    }
}