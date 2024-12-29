using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FinTracker.Logic.Models.Payment.Enums;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

public record struct UpdatePaymentRequest : IValidatableObject
{
    [Required]
    [MaxLength(128)]
    public required string Title { get; init; }

    [Required]
    [MaxLength(1024)]
    public required string Description { get; init; }
    
    [Required]
    public required Guid BillId { get; init; }

    [Required]
    public required decimal Amount { get; init; }

    [Required]
    public required FinancialOperation Operation { get; init; }
    
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