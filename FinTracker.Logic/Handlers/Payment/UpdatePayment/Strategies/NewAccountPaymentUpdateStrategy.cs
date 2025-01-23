using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Extensions;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment.Strategies;

public class NewAccountPaymentUpdateStrategy : IPaymentUpdateStrategy
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IAccountRepository accountRepository;

    public NewAccountPaymentUpdateStrategy(IPaymentRepository paymentRepository, IAccountRepository accountRepository)
    {
        this.paymentRepository = paymentRepository;
        this.accountRepository = accountRepository;
    }
    
    public bool Accept(UpdatePaymentCommand updateCommand)
    {
        return updateCommand.AccountId != null;
    }

    public async Task UpdateAsync(Dal.Models.Payments.Payment payment, UpdatePaymentCommand updateCommand)
    {
        var newAccount = (await this.accountRepository.SearchAsync(AccountSearch.ById(updateCommand!.AccountId!.Value!))).FirstOrDefault();
        var updatedPayment = payment.GetUpdated(updateCommand);
        
        if (payment.AccountId != null)
        {
            var oldAccount = (await this.accountRepository.SearchAsync(AccountSearch.ById(payment.AccountId.Value))).FirstOrDefault();

            if (oldAccount.CurrencyId != newAccount.CurrencyId)
            {
                throw new ForbiddenOperation("Can't transfer payment between accounts with different currencies.");
            }
            
            oldAccount.RevertPayment(payment);
            
            (await this.accountRepository.UpdateAsync(oldAccount)).EnsureSuccess();
        }
        
        newAccount.ApplyPayment(updatedPayment);
        
        (await this.accountRepository.UpdateAsync(newAccount)).EnsureSuccess();
        (await this.paymentRepository.UpdateAsync(updatedPayment)).EnsureSuccess();
    }
}