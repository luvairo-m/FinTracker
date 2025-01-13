using System;
using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Bill.Dto.Requests;

public class UpdateBillRequest
{
    [MaxLength(128)]
    public string Title { get; init; }
    
    [MaxLength(1024)]
    public string Description { get; init; }
    
    public Guid? CurrencyId { get; init; }
}