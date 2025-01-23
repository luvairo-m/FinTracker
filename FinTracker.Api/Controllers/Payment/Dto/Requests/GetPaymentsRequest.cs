using System;
using System.ComponentModel.DataAnnotations;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Requests;

public class GetPaymentsRequest
{
    [MaxLength(128)]
    public string TitleSubstring { get; set; }

    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? MinAmount { get; set; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? MaxAmount { get; set; }
    
    public OperationType[] Types { get; set; }

    public DateTime? MinDate { get; set; }

    public DateTime? MaxDate { get; set; }

    public int[] Months { get; set; }

    public int[] Years { get; set; }

    public Guid? AccountId { get; set; }
    
    public Guid[] Categories { get; set; }
}