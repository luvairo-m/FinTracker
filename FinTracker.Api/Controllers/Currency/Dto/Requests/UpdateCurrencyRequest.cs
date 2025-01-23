using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Currency.Dto.Requests;

public class UpdateCurrencyRequest
{
    [MaxLength(64)]
    public string Title { get; init; }

    [MaxLength(6)]
    public string Sign { get; init; }
}