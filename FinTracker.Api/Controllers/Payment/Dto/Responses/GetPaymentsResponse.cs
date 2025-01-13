using System.Collections.Generic;

namespace FinTracker.Api.Controllers.Payment.Dto.Responses;

public class GetPaymentsResponse
{
    public ICollection<GetPaymentResponse> Payments { get; init; }
}