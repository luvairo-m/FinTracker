using System.Collections.Generic;

namespace FinTracker.Api.Controllers.Currency.Dto.Responses;

public record struct GetCurrenciesResponse
{
    public required ICollection<GetCurrencyResponse> Currencies { get; set; }
}