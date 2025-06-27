using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    public decimal TotalAmount => Items.Sum(item => item.TotalAmount);
    public SaleStatus Status { get; set; } = SaleStatus.Active;

    public override async Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        var validator = new SaleValidator();

        var validationResult = await validator.ValidateAsync(this);

        if (validationResult.IsValid)
            return Enumerable.Empty<ValidationErrorDetail>();

        return validationResult.Errors.Select(error => new ValidationErrorDetail
        {
            Error = error.PropertyName, 
            Detail = error.ErrorMessage
        });
    }
}