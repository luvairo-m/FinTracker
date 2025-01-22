using System.Transactions;
using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Utils;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.RemovePayment;

// ReSharper disable once UnusedType.Global
internal class RemovePaymentCommandHandler : IRequestHandler<RemovePaymentCommand>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public RemovePaymentCommandHandler(IPaymentRepository paymentRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task Handle(RemovePaymentCommand request, CancellationToken cancellationToken)
    {
        using var transactionScope = TransactionUtils.CreateTransactionScope(IsolationLevel.RepeatableRead);
        
        var getPaymentResult = await paymentRepository.SearchAsync(new PaymentSearch { Id = request.Id });
        getPaymentResult.EnsureSuccess();
        
        var payment = getPaymentResult.Result.FirstOrDefault();

        if (payment.AccountId != null)
        {
            await this.RemovePaymentFromAccount(payment);
        }
        
        var deletePaymentResult = await paymentRepository.RemoveAsync(request.Id);
        deletePaymentResult.EnsureSuccess();
        
        transactionScope.Complete();
    }

    private async Task RemovePaymentFromAccount(Dal.Models.Payments.Payment payment)
    {
        var getAccountResult = await accountRepository.SearchAsync(new AccountSearch { Id = payment!.AccountId });
        getAccountResult.EnsureSuccess();
        
        var account = getAccountResult.Result.FirstOrDefault();
        
        PaymentUtils.EnsureRevertPayment(account.Balance.Value, payment.Amount.Value, payment.Type.Value);
            
        account!.Balance = payment!.Type == OperationType.Outcome 
            ? account.Balance + payment.Amount 
            : account.Balance - payment.Amount;
        
        var updateAccountResult = await accountRepository.UpdateAsync(account);
        updateAccountResult.EnsureSuccess();
    }
}