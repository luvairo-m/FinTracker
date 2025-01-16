using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

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
        var gettingPaymentResult = await paymentRepository.SearchAsync(mapper.Map<PaymentSearch>(request));
        gettingPaymentResult.EnsureSuccess();

        var payment = gettingPaymentResult.Result.FirstOrDefault();
        
        var gettingCurrentBillResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(payment));
        gettingCurrentBillResult.EnsureSuccess();
        
        var currentBill = gettingCurrentBillResult.Result.FirstOrDefault();
        
        var updatedPayment = mapper.Map<Dal.Models.Payments.Payment>(request);

        if (!request.AccountId.HasValue && request.Amount.HasValue)
        {
            if (payment.Type == OperationType.Outcome && currentBill!.Balance < request.Amount)
            {
                throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
            }
            
            currentBill.Balance = payment.Type == OperationType.Outcome
                ? currentBill.Balance - payment.Amount 
                : currentBill.Balance + payment.Amount;
            
            var updatedBillResult = await accountRepository.UpdateAsync(currentBill);
            updatedBillResult.EnsureSuccess();
        }

        if (request.AccountId.HasValue && !request.Amount.HasValue)
        {
            var gettingNewBillResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));
            gettingNewBillResult.EnsureSuccess();
            
            var newBill = gettingNewBillResult.Result.FirstOrDefault();

            if (payment.Type == OperationType.Outcome && newBill!.Balance < payment!.Amount)
            {
                throw new ForbiddenOperation(
                    $"Insufficient funds in the account with title '{newBill.Title}' to update the payment.");
            }
            
            currentBill!.Balance = payment.Type == OperationType.Outcome 
                ? currentBill.Balance + payment.Amount 
                : currentBill.Balance - payment.Amount;
            
            var updatedCurrentBillResult = await accountRepository.UpdateAsync(currentBill);
            updatedCurrentBillResult.EnsureSuccess();
            
            newBill.Balance = payment.Type == OperationType.Outcome 
                ? newBill.Balance - payment.Amount 
                : newBill.Balance + payment.Amount;;
            
            var updatedNewBillResult = await accountRepository.UpdateAsync(newBill);
            updatedNewBillResult.EnsureSuccess();
        }

        if (request.AccountId.HasValue && request.Amount.HasValue)
        {
            var gettingNewBillResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));
            gettingNewBillResult.EnsureSuccess();
            
            var newBill = gettingNewBillResult.Result.FirstOrDefault();

            if (payment.Type == OperationType.Outcome && newBill!.Balance < payment!.Amount)
            {
                throw new ForbiddenOperation(
                    $"Insufficient funds in the account with title '{newBill.Title}' to update the payment.");
            }
            
            var differenceBetweenAmount = Math.Abs((decimal)(payment.Amount - (decimal)request.Amount)!);
            
            currentBill!.Balance = payment.Type == OperationType.Outcome 
                ? currentBill.Balance + differenceBetweenAmount 
                : currentBill.Balance - differenceBetweenAmount;
            
            var updatedCurrentBillResult = await accountRepository.UpdateAsync(currentBill);
            updatedCurrentBillResult.EnsureSuccess();
            
            newBill.Balance = payment.Type == OperationType.Outcome 
                ? newBill.Balance - differenceBetweenAmount
                : newBill.Balance + differenceBetweenAmount;
            
            var updatedNewBillResult = await accountRepository.UpdateAsync(newBill);
            updatedNewBillResult.EnsureSuccess();
        }
        
        var updatingPaymentResult = await paymentRepository.UpdateAsync(updatedPayment);
        updatingPaymentResult.EnsureSuccess();
    }
}