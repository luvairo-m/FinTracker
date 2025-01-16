using System;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Api.Controllers.Payment.Dto.Responses;

public class GetPaymentResponse
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }

    public string Description { get; init; }
    
    public decimal Amount { get; init; }

    public Guid AccountId { get; init; }
    
    public OperationType Type { get; init; }

    public DateTime Date { get; init; }
}