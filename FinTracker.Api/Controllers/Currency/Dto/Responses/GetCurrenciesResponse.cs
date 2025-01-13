using System.Collections.Generic;

namespace FinTracker.Api.Controllers.Currency.Dto.Responses;

public class GetCurrenciesResponse
{
    public ICollection<GetCurrencyResponse> Currencies { get; set; }
}