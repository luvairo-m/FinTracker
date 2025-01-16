using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Currency.Dto.Requests;

public class UpdateCurrencyRequest
{
    [MaxLength(128)]
    public string Title { get; init; }

    [MaxLength(10)]
    public string Sign { get; init; }
}