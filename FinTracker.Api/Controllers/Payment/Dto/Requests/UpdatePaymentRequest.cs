using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

public record struct UpdatePaymentRequest : IValidatableObject
{
    [MaxLength(128)]
    public required string Title { get; init; }
    
    [MaxLength(1024)]
    public required string Description { get; init; }
    
    [Required]
    public required decimal? Amount { get; init; }
    
    [Required]
    public required Guid? BillId { get; init; }

    [Required]
    public required Guid? CategoryId { get; init; }

    [Required]
    public required OperationType? Type { get; init; }
    
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