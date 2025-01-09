using System;

namespace FinTracker.Api.Controllers.Bill.Dto.Responses;

public record struct GetBillResponse
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public required decimal Balance { get; init; }
    
    public required string Description { get; init; }
    
    public required Guid CurrencyId { get; init; }
}