using FinTracker.Logic.Models.Payment.Enums;

namespace FinTracker.Logic.Models.Payment.Results;

public struct GetPaymentResult
{
    public Guid PaymentId { get; init; }
    
    public string Title { get; init; }

    public string Description { get; init; }

    public Guid BillId { get; init; }
    
    public int Amount { get; init; }
    
    public FinancialOperation Operation { get; init; }

    public DateTime PaymentDate { get; init; }
}