using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
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
        var gettingPaymentsResult = await paymentRepository.SearchAsync(mapper.Map<PaymentSearch>(request));
        gettingPaymentsResult.EnsureSuccess();
        
        var existingPayment = gettingPaymentsResult.Result.FirstOrDefault();

        var gettingAccountsResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(existingPayment));
        gettingAccountsResult.EnsureSuccess();
        
        var existingAccount = gettingAccountsResult.Result.FirstOrDefault();
        
        var deletionPaymentResult = await paymentRepository.RemoveAsync(request.Id);
        deletionPaymentResult.EnsureSuccess();
        
        existingAccount!.Balance = existingPayment!.Type == OperationType.Outcome 
            ? existingAccount.Balance + existingPayment.Amount 
            : existingAccount.Balance - existingPayment.Amount;
        
        var updatingAccountResult = await accountRepository.UpdateAsync(existingAccount);
        updatingAccountResult.EnsureSuccess();
    }
}