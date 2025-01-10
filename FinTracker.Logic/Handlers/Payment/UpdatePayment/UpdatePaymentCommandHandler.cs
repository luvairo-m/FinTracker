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
        var gettingBillResult = await _billRepository.SearchAsync(new BillSearch { Id = request.BillId });
        gettingBillResult.EnsureSuccess();

        var bill = gettingBillResult.Result.FirstOrDefault();

        if (request.Amount.HasValue && bill!.Balance < request.Amount)
        {
            throw new ForbiddenOperation("Insufficient funds to complete the transaction.");
        }

        var updatedPayment = _mapper.Map<Dal.Models.Payments.Payment>(request);
        
        var updatingPaymentResult = await _paymentRepository.UpdateAsync(updatedPayment);
        updatingPaymentResult.EnsureSuccess();
        
        bill!.Balance -= request.Amount;
        
        var updatedBillResult = await _billRepository.UpdateAsync(bill);
        updatedBillResult.EnsureSuccess();
    }
}