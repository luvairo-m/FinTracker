namespace FinTracker.Logic.Models.Payment.Results;

public struct GetPaymentsResult
{
    public ICollection<GetPaymentResult> Payments { get; set; }
}