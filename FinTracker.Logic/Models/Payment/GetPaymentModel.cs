using FinTracker.Dal.Models.Payments;

namespace FinTracker.Logic.Models.Payment;

public struct GetPaymentModel
{
    public Guid Id { get; init; }
    
    public string Title { get; init; }

    public string Description { get; init; }
    
    public decimal Amount { get; init; }

    public Guid BillId { get; init; }
    
    public OperationType Type { get; init; }

    public DateTime Date { get; init; }
}