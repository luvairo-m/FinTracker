using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Currency.Dto.Requests;

public record struct UpdateCurrencyRequest
{
    [Required]
    [MaxLength(128)]
    public required string Title { get; init; }

    [Required]
    [MaxLength(10)]
    public required string Sign { get; init; }
}