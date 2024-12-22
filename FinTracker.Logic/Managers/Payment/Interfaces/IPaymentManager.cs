using FinTracker.Logic.Models.Payment.Params;
using FinTracker.Logic.Models.Payment.Results;

namespace FinTracker.Logic.Managers.Payment.Interfaces;

public interface IPaymentManager
{
    Task<CreatePaymentResult> CreatePayment(CreatePaymentParam createPaymentParam);
    
    Task<GetPaymentResult> GetPayment(Guid paymentId);

    Task<GetPaymentsResult> GetPayments();
    
    Task UpdatePayment(UpdatePaymentParam updatePaymentParam);
    
    Task DeletePayment(Guid paymentId);
}