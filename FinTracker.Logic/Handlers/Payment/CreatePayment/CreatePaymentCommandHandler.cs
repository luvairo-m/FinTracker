using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

// ReSharper disable once UnusedType.Global
internal class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentModel>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task<CreatePaymentModel> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        using var transactionScope = TransactionUtils.CreateTransactionScope();
        
        var getAccountResult = await accountRepository.SearchAsync(new AccountSearch { Id = request.AccountId });
        getAccountResult.EnsureSuccess();

        var account = getAccountResult.Result.FirstOrDefault();

        if (request.Type == OperationType.Outcome && account!.Balance < request.Amount)
        {
            throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
        }
        
        var addPaymentResult = await paymentRepository.AddAsync(mapper.Map<Dal.Models.Payments.Payment>(request));
        addPaymentResult.EnsureSuccess();
        
        account!.Balance = request.Type == OperationType.Outcome
            ? account.Balance - request.Amount 
            : account.Balance + request.Amount;

        var updateAccountResult = await accountRepository.UpdateAsync(account);
        updateAccountResult.EnsureSuccess();
        
        transactionScope.Complete();

        return new CreatePaymentModel { PaymentId = addPaymentResult.Result };
    }
}