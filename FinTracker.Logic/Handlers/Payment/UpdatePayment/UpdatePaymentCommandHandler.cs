using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Dal.Repositories.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

internal class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBillRepository _billRepository;
    private readonly IMapper _mapper;

    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository, IBillRepository billRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _billRepository = billRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var gettingPaymentResult = await _paymentRepository.SearchAsync(new PaymentSearch { Id = request.Id });
        gettingPaymentResult.EnsureSuccess();

        var payment = gettingPaymentResult.Result.FirstOrDefault();
        
        var gettingCurrentBillResult = await _billRepository.SearchAsync(new BillSearch { Id = payment?.BillId });
        gettingCurrentBillResult.EnsureSuccess();
        
        var currentBill = gettingCurrentBillResult.Result.FirstOrDefault();
        
        var updatedPayment = _mapper.Map<Dal.Models.Payments.Payment>(request);

        if (!request.BillId.HasValue && request.Amount.HasValue)
        {
            if (currentBill!.Balance < request.Amount)
            {
                throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
            }
            
            var updatingPaymentResult = await _paymentRepository.UpdateAsync(updatedPayment);
            updatingPaymentResult.EnsureSuccess();
            
            currentBill.Balance = payment.Type == OperationType.Outcome
                ? currentBill.Balance - payment.Amount 
                : currentBill.Balance + payment.Amount;
            
            var updatedBillResult = await _billRepository.UpdateAsync(currentBill);
            updatedBillResult.EnsureSuccess();
        }

        if (request.BillId.HasValue && !request.Amount.HasValue)
        {
            var gettingNewBillResult = await _billRepository.SearchAsync(new BillSearch { Id = request.BillId });
            gettingNewBillResult.EnsureSuccess();
            
            var newBill = gettingNewBillResult.Result.FirstOrDefault();

            if (newBill!.Balance < payment!.Amount)
            {
                throw new ForbiddenOperation(
                    $"Insufficient funds in the account with title '{newBill.Title}' to update the payment.");
            }
            
            var updatingPaymentResult = await _paymentRepository.UpdateAsync(updatedPayment);
            updatingPaymentResult.EnsureSuccess();
            
            currentBill!.Balance = payment.Type == OperationType.Outcome 
                ? currentBill.Balance + payment.Amount 
                : currentBill.Balance - payment.Amount;
            
            var updatedCurrentBillResult = await _billRepository.UpdateAsync(currentBill);
            updatedCurrentBillResult.EnsureSuccess();
            
            newBill.Balance = payment.Type == OperationType.Outcome 
                ? newBill.Balance - payment.Amount 
                : newBill.Balance + payment.Amount;;
            
            var updatedNewBillResult = await _billRepository.UpdateAsync(newBill);
            updatedNewBillResult.EnsureSuccess();
        }

        if (request.BillId.HasValue && request.Amount.HasValue)
        {
            var gettingNewBillResult = await _billRepository.SearchAsync(new BillSearch { Id = request.BillId });
            gettingNewBillResult.EnsureSuccess();
            
            var newBill = gettingNewBillResult.Result.FirstOrDefault();

            if (newBill!.Balance < payment!.Amount)
            {
                throw new ForbiddenOperation(
                    $"Insufficient funds in the account with title '{newBill.Title}' to update the payment.");
            }
            
            var updatingPaymentResult = await _paymentRepository.UpdateAsync(updatedPayment);
            updatingPaymentResult.EnsureSuccess();
            
            var differenceBetweenAmount = Math.Abs((decimal)(payment.Amount - (decimal)request.Amount)!);
            
            currentBill!.Balance = payment.Type == OperationType.Outcome 
                ? currentBill.Balance + differenceBetweenAmount 
                : currentBill.Balance - differenceBetweenAmount;
            
            var updatedCurrentBillResult = await _billRepository.UpdateAsync(currentBill);
            updatedCurrentBillResult.EnsureSuccess();
            
            newBill.Balance = payment.Type == OperationType.Outcome 
                ? newBill.Balance - differenceBetweenAmount
                : newBill.Balance + differenceBetweenAmount;
            
            var updatedNewBillResult = await _billRepository.UpdateAsync(newBill);
            updatedNewBillResult.EnsureSuccess();
        }
    }
}