using System;
using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Bill.Dto.Requests;

public class CreateBillRequest
{
    [Required]
    [MaxLength(128)]
    public string Title { get; init; }

    [Required]
    public decimal? Balance { get; init; }

    [MaxLength(1024)]
    public string Description { get; init; }

    [Required]
    public Guid? CurrencyId { get; init; }
}