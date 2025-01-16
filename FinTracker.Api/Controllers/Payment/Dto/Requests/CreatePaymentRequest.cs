using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

public class CreatePaymentRequest : IValidatableObject
{
    [Required]
    [MaxLength(128)]
    public string Title { get; init; }
    
    [MaxLength(1024)]
    public string Description { get; init; }
    
    [Required]
    public decimal? Amount { get; init; }
    
    [Required]
    public Guid? AccountId { get; init; }

    [Required]
    public Guid? CategoryId { get; init; }

    [Required]
    public OperationType? Type { get; init; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = Array.Empty<ValidationResult>();

        if (Amount < 0)
        {
            return [new ValidationResult("Сумма платежа не может быть отрицательной", [nameof(Amount)])];
        }

        return validationResults;
    }
}