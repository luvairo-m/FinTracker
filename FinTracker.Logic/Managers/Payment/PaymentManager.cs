using FinTracker.Logic.Managers.Payment.Interfaces;
using FinTracker.Logic.Models.Payment.Params;
using FinTracker.Logic.Models.Payment.Results;

namespace FinTracker.Logic.Managers.Payment;

public class PaymentManager : IPaymentManager
{
    public Task<CreatePaymentResult> CreatePayment(CreatePaymentParam createPaymentParam)
    {
        throw new NotImplementedException();
    }

    public Task<GetPaymentResult> GetPayment(Guid paymentId)
    {
        throw new NotImplementedException();
    }

    public Task<GetPaymentsResult> GetPayments()
    {
        throw new NotImplementedException();
    }

    public Task UpdatePayment(UpdatePaymentParam updatePaymentParam)
    {
        throw new NotImplementedException();
    }

    public Task DeletePayment(Guid paymentId)
    {
        throw new NotImplementedException();
    }
}