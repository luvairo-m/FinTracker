using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Currency.Dto.Requests;

public class CreateCurrencyRequest
{
    [Required]
    [MaxLength(64)]
    public string Title { get; init; }

    [Required]
    [MaxLength(6)]
    public string Sign { get; init; }
}