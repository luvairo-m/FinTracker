using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

internal class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentModel>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IBillRepository billRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IBillRepository billRepository)
    {
        this.paymentRepository = paymentRepository;
        this.billRepository = billRepository;
    }

    public async Task<CreatePaymentModel> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var newPayment = new Dal.Models.Payments.Payment
        {
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
            Type = request.Type,
            Date = DateTime.UtcNow,
            BillId = request.BillId
        };
        
        var gettingBillResult = await billRepository.SearchAsync(new BillSearch { Id = request.BillId });
        gettingBillResult.EnsureSuccess();

        var bill = gettingBillResult.Result.FirstOrDefault();

        if (bill!.Balance < request.Amount)
        {
            throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
        }
        
        var addedPaymentResult = await paymentRepository.AddAsync(newPayment);
        addedPaymentResult.EnsureSuccess();
        
        bill.Balance -= request.Amount;
        
        var updatedBillResult = await billRepository.UpdateAsync(bill);
        updatedBillResult.EnsureSuccess();

        return new CreatePaymentModel { PaymentId = addedPaymentResult.Result };
    }
}