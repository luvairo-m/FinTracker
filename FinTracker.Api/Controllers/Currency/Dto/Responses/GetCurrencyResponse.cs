using System;

namespace FinTracker.Api.Controllers.Currency.Dto.Responses;

public record struct GetCurrencyResponse
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Sign { get; set; }
}