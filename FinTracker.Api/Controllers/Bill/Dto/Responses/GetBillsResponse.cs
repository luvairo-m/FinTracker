using System.Collections.Generic;

namespace FinTracker.Api.Controllers.Bill.Dto.Responses;

public record struct GetBillsResponse
{
    public ICollection<GetBillResponse> Bills { get; init; }
}