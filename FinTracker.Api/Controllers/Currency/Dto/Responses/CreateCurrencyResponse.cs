using System;

namespace FinTracker.Api.Controllers.Currency.Dto.Responses;

public record struct CreateCurrencyResponse
{
    public required Guid CurrencyId { get; init; }
}