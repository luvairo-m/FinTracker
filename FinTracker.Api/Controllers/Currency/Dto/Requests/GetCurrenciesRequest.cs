using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Currency.Dto.Requests;

public class GetCurrenciesRequest
{
    [MaxLength(64)]
    public string TitleSubstring { get; init; }
}