using System.Collections.Generic;

namespace FinTracker.Api.Controllers.Payment.Dto.Responses;

public record struct GetPaymentsResponse
{
    public required ICollection<GetPaymentResponse> Payments { get; init; }
}