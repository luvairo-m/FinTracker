namespace FinTracker.Logic.Models.Payment;

public struct GetPaymentsModel
{
    public ICollection<GetPaymentModel> Payments { get; set; }
}