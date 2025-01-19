using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

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
        var newPayment = mapper.Map<Dal.Models.Payments.Payment>(request);
        
        var getAccountResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));
        getAccountResult.EnsureSuccess();

        var account = getAccountResult.Result.FirstOrDefault();

        if (request.Type == OperationType.Outcome && account!.Balance < request.Amount)
        {
            throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
        }
        
        var addPaymentResult = await paymentRepository.AddAsync(newPayment);
        addPaymentResult.EnsureSuccess();
        
        account!.Balance = request.Type == OperationType.Outcome
            ? account.Balance - request.Amount 
            : account.Balance + request.Amount;
        
        var updateAccountResult = await accountRepository.UpdateAsync(account);
        updateAccountResult.EnsureSuccess();

        return new CreatePaymentModel { PaymentId = addPaymentResult.Result };
    }
}