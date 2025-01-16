using System.ComponentModel.DataAnnotations;

namespace FinTracker.Api.Controllers.Currency.Dto.Requests;

public class GetCurrenciesRequest
{
    [MaxLength(256)]
    public string TitleSubstring { get; init; }
}