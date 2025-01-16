using System;
using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Account.Dto.Requests;

public class CreateAccountRequest
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