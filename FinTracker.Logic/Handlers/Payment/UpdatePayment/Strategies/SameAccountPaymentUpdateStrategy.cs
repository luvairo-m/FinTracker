using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Extensions;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment.Strategies;

public class SameAccountPaymentUpdateStrategy : IPaymentUpdateStrategy
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IAccountRepository accountRepository;

    public SameAccountPaymentUpdateStrategy(IPaymentRepository paymentRepository, IAccountRepository accountRepository)
    {
        this.paymentRepository = paymentRepository;
        this.accountRepository = accountRepository;
    }
    
    public bool Accept(UpdatePaymentCommand updateCommand)
    {
        return (updateCommand.Amount != null || updateCommand.Type != null) && updateCommand.AccountId == null;
    }

    public async Task UpdateAsync(Dal.Models.Payments.Payment payment, UpdatePaymentCommand updateCommand)
    {
        var updatedPayment = payment.GetUpdated(updateCommand);
        
        if (payment.AccountId != null)
        {
            var account = (await this.accountRepository.SearchAsync(AccountSearch.ById(payment.AccountId.Value))).FirstOrDefault();
            
            account.RevertPayment(payment);
            account.ApplyPayment(updatedPayment);

            (await this.accountRepository.UpdateAsync(account)).EnsureSuccess();
        }
        
        (await this.paymentRepository.UpdateAsync(updatedPayment)).EnsureSuccess();
    }
}