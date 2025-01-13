using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

public class UpdatePaymentRequest : IValidatableObject
{
    [MaxLength(128)]
    public string Title { get; init; }
    
    [MaxLength(1024)]
    public string Description { get; init; }
    
    public decimal? Amount { get; init; }
    
    public Guid? BillId { get; init; }
    
    public Guid? CategoryId { get; init; }
    
    public OperationType? Type { get; init; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = Array.Empty<ValidationResult>();

        if (Amount.HasValue && Amount.Value < 0)
        {
            return [new ValidationResult("Сумма платежа не может быть отрицательной", [nameof(Amount)])];
        }

        return validationResults;
    }
}