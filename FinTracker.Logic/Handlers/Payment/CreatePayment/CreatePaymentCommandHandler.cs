using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

internal class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentModel>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBillRepository _billRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IBillRepository billRepository)
    {
        _paymentRepository = paymentRepository;
        _billRepository = billRepository;
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

        var addedPaymentResult = await _paymentRepository.AddAsync(newPayment);
        addedPaymentResult.EnsureSuccess();

        var gettingBillResult = await _billRepository.SearchAsync(new BillSearch { Id = request.BillId });
        gettingBillResult.EnsureSuccess();

        var bill = gettingBillResult.Result.FirstOrDefault();
        bill!.Balance -= request.Amount;
        
        var updatedBillResult = await _billRepository.UpdateAsync(bill);
        updatedBillResult.EnsureSuccess();

        return new CreatePaymentModel { PaymentId = addedPaymentResult.Result };
    }
}