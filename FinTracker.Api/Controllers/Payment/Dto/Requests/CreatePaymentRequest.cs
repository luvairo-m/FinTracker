using System;
using System.ComponentModel.DataAnnotations;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

public class CreatePaymentRequest
{
    [Required]
    [MaxLength(128)]
    public string Title { get; init; }
    
    [MaxLength(1024)]
    public string Description { get; init; }
    
    [Required]
    [Range(typeof(decimal), "1", "79228162514264337593543950335")]
    public decimal? Amount { get; init; }
    
    public Guid? AccountId { get; init; }

    [Required]
    public OperationType? Type { get; init; }
    
    public Guid[] Categories { get; init; }
}