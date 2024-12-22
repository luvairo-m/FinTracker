using FinTracker.Logic.Models.Payment.Enums;

namespace FinTracker.Logic.Models.Payment.Params;

public struct UpdatePaymentParam
{
    public Guid PaymentId { get; set; }
    
    public string Title { get; init; }
    
    public string Description { get; init; }
    
    public Guid BillId { get; init; }
    
    public int Amount { get; init; }
    
    public FinancialOperation Operation { get; init; }
}