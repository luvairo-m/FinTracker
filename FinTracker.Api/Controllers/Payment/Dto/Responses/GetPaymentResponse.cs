using System;
using FinTracker.Logic.Models.Payment.Enums;

namespace FinTracker.Api.Controllers.Payment.Dto.Responses;

public record struct GetPaymentResponse
{
    public required Guid PaymentId { get; init; }
    
    public required string Title { get; init; }

    public required string Description { get; init; }

    public required Guid BillId { get; init; }
    
    public required int Amount { get; init; }
    
    public required FinancialOperation Operation { get; init; }

    public required DateTime PaymentDate { get; init; }
}