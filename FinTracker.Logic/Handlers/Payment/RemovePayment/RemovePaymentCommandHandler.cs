using System.Transactions;
using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Extensions;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.RemovePayment;

// ReSharper disable once UnusedType.Global
internal class RemovePaymentCommandHandler : IRequestHandler<RemovePaymentCommand>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IAccountRepository accountRepository;

    public RemovePaymentCommandHandler(IPaymentRepository paymentRepository, IAccountRepository accountRepository)
    {
        this.paymentRepository = paymentRepository;
        this.accountRepository = accountRepository;
    }

    public async Task Handle(RemovePaymentCommand request, CancellationToken cancellationToken)
    {
        using var transactionScope = TransactionUtils.CreateTransactionScope(IsolationLevel.RepeatableRead);
        
        var payment = (await paymentRepository.SearchAsync(PaymentSearch.ById(request.Id))).FirstOrDefault();

        if (payment.AccountId != null)
        {
            var account = (await accountRepository.SearchAsync(AccountSearch.ById(payment.AccountId.Value))).FirstOrDefault();
            account.RevertPayment(payment);
        
            (await accountRepository.UpdateAsync(account)).EnsureSuccess();
        }
        
        (await paymentRepository.RemoveAsync(request.Id)).EnsureSuccess();
        
        transactionScope.Complete();
    }
}