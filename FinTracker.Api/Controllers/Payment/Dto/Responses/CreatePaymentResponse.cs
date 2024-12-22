using System;

namespace FinTracker.Api.Controllers.Payment.Dto.Responses;

public record struct CreatePaymentResponse
{
    public required Guid PaymentId { get; init; }
}