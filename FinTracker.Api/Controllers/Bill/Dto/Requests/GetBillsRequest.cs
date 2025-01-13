using System;
using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Bill.Dto.Requests;

public class GetBillsRequest
{
    public Guid? Id { get; init; }

    [MaxLength(128)]
    public string Title { get; init; }
    
    public Guid? CurrencyId { get; init; }
}