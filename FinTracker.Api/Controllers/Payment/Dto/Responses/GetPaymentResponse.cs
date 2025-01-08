using System;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Responses;

public record struct GetPaymentResponse
{
    public required Guid PaymentId { get; init; }
    
    public required string Title { get; init; }

    public required string Description { get; init; }
    
    public required decimal Amount { get; init; }

    public required Guid BillId { get; init; }

    public required Guid CategoryId { get; init; }
    
    public required OperationType Type { get; init; }

    public required DateTime Date { get; init; }
}