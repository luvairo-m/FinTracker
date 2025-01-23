using System;
using System.ComponentModel.DataAnnotations;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

// ReSharper disable once UnusedType.Global
public class UpdatePaymentRequest
{
    [MaxLength(128)]
    public string Title { get; init; }
    
    [MaxLength(1024)]
    public string Description { get; init; }
    
    [Range(typeof(decimal), "1", "79228162514264337593543950335")]
    public decimal? Amount { get; init; }
    
    public Guid? AccountId { get; init; }
    
    public OperationType? Type { get; init; }
    
    public DateTime? Date { get; set; }
}