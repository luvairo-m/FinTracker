using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

// ReSharper disable once UnusedType.Global
internal class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var getPaymentResult = await paymentRepository.SearchAsync(mapper.Map<PaymentSearch>(request));
        getPaymentResult.EnsureSuccess();

        var payment = getPaymentResult.Result.FirstOrDefault()!;
        
        var getCurrentAccountResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(payment));
        getCurrentAccountResult.EnsureSuccess();
        
        var currentAccount = getCurrentAccountResult.Result.FirstOrDefault()!;
        
        var updatedPayment = mapper.Map<Dal.Models.Payments.Payment>(request);

        if (!request.AccountId.HasValue && request.Amount.HasValue)
        {
            if (payment.Type == OperationType.Outcome && currentAccount!.Balance < request.Amount)
            {
                throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
            }
            
            currentAccount.Balance = payment.Type == OperationType.Outcome
                ? currentAccount.Balance - payment.Amount 
                : currentAccount.Balance + payment.Amount;
            
            (await accountRepository.UpdateAsync(currentAccount)).EnsureSuccess();
        }

        if (request is { AccountId: not null, Amount: null })
        {
            var getAccountResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));
            getAccountResult.EnsureSuccess();
            
            var account = getAccountResult.Result.FirstOrDefault()!;

            if (payment.Type == OperationType.Outcome && account!.Balance < payment!.Amount)
            {
                throw new ForbiddenOperation(
                    $"Insufficient funds in the account with title '{account.Title}' to update the payment.");
            }
            
            currentAccount!.Balance = payment.Type == OperationType.Outcome 
                ? currentAccount.Balance + payment.Amount 
                : currentAccount.Balance - payment.Amount;
            
            (await accountRepository.UpdateAsync(currentAccount)).EnsureSuccess();
            
            account.Balance = payment.Type == OperationType.Outcome 
                ? account.Balance - payment.Amount 
                : account.Balance + payment.Amount;;
            
            (await accountRepository.UpdateAsync(account)).EnsureSuccess();
        }

        if (request is { AccountId: not null, Amount: not null })
        {
            var getNewAccountResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));
            getNewAccountResult.EnsureSuccess();
            
            var account = getNewAccountResult.Result.FirstOrDefault()!;

            if (payment.Type == OperationType.Outcome && account!.Balance < payment!.Amount)
            {
                throw new ForbiddenOperation(
                    $"Insufficient funds in the account with title '{account.Title}' to update the payment.");
            }
            
            var differenceBetweenAmount = Math.Abs((decimal)(payment.Amount - (decimal)request.Amount)!);
            
            currentAccount!.Balance = payment.Type == OperationType.Outcome 
                ? currentAccount.Balance + differenceBetweenAmount 
                : currentAccount.Balance - differenceBetweenAmount;
            
            (await accountRepository.UpdateAsync(currentAccount)).EnsureSuccess();
            
            account.Balance = payment.Type == OperationType.Outcome 
                ? account.Balance - differenceBetweenAmount
                : account.Balance + differenceBetweenAmount;
            
            (await accountRepository.UpdateAsync(account)).EnsureSuccess();
        }
        
        (await paymentRepository.UpdateAsync(updatedPayment)).EnsureSuccess();
    }
}