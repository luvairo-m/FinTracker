using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Currency.Dto.Requests;

public class CreateCurrencyRequest
{
    [Required]
    [MaxLength(128)]
    public string Title { get; init; }

    [Required]
    [MaxLength(10)]
    public string Sign { get; init; }
}