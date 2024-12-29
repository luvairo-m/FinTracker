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
        var validationResults = new List<ValidationResult>(2);
        
        if (Operation == FinancialOperation.Expense && Amount > 0)
        {
            validationResults.Add(new ValidationResult(
                "Сумма не может быть положительной для операции расхода", 
                [nameof(Amount)]));
        }

        if (Operation == FinancialOperation.Income && Amount < 0)
        {
            validationResults.Add(new ValidationResult(
                "Сумма не может быть отрицательной для операции дохода", 
                [nameof(Amount)]));
        }

        return validationResults;
    }
}