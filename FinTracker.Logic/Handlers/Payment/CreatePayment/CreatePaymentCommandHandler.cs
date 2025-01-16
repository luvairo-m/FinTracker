using AutoMapper;
using FinTracker.Dal.Models.Bills;
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
        
        var gettingAccountResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));
        gettingAccountResult.EnsureSuccess();

        var bill = gettingAccountResult.Result.FirstOrDefault();

        if (request.Type == OperationType.Outcome && bill!.Balance < request.Amount)
        {
            throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
        }
        
        var addedPaymentResult = await paymentRepository.AddAsync(newPayment);
        addedPaymentResult.EnsureSuccess();
        
        bill!.Balance = request.Type == OperationType.Outcome
            ? bill.Balance - request.Amount 
            : bill.Balance + request.Amount;
        
        var updatedBillResult = await accountRepository.UpdateAsync(bill);
        updatedBillResult.EnsureSuccess();

        return new CreatePaymentModel { PaymentId = addedPaymentResult.Result };
    }
}