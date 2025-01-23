using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Extensions;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment.Strategies;

public class NonFinancialPaymentUpdateStrategy : IPaymentUpdateStrategy
{
    private readonly IPaymentRepository paymentRepository;

    public NonFinancialPaymentUpdateStrategy(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }
    
    public bool Accept(UpdatePaymentCommand updateCommand)
    {
        return updateCommand.Amount == null && updateCommand.AccountId == null && updateCommand.Type == null;
    }

    public async Task UpdateAsync(Dal.Models.Payments.Payment payment, UpdatePaymentCommand updateCommand)
    {
        (await this.paymentRepository.UpdateAsync(payment.GetUpdated(updateCommand))).EnsureSuccess();
    }
}