using System;

namespace FinTracker.Api.Controllers.Bill.Dto.Responses;

public record struct GetBillResponse
{
    public required Guid BillId { get; init; }

    public required string Title { get; init; }

    public required int Amount { get; init; }
}