using FinTracker.Dal.Models.Payments;

namespace FinTracker.Logic.Models.Payment;

public struct GetPaymentModel
{
    public Guid PaymentId { get; init; }
    
    public string Title { get; init; }

    public string Description { get; init; }
    
    public decimal Amount { get; init; }

    public Guid BillId { get; init; }

    public Guid CurrencyId { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public OperationType Type { get; init; }

    public DateTime Date { get; init; }
}