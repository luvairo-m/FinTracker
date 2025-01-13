using System;

namespace FinTracker.Api.Controllers.Currency.Dto.Responses;

public class GetCurrencyResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Sign { get; set; }
}