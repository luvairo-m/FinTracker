using System;

namespace FinTracker.Api.Controllers.Bill.Dto.Responses;

public record struct CreateBillResponse
{
    public required Guid BillId { get; init; }
}