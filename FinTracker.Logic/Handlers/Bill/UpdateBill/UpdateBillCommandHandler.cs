using AutoMapper;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Dal.Repositories.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.UpdateBill;

internal class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand>
{
    private readonly IBillRepository billRepository;
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public UpdateBillCommandHandler(
        IBillRepository billRepository, 
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        this.billRepository = billRepository;
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateBillCommand request, CancellationToken cancellationToken)
    {
        if (request.CurrencyId != null)
        {
            var searchResult = await this.paymentRepository.SearchAsync(new PaymentSearch { BillId = request.Id });
            searchResult.EnsureSuccess();

            // Пока не умеем пересчитывать платежи в рамках счета на другую валюту.
            if (searchResult.Result.Count > 0)
            {
                throw new Exception("Currency updating can be applied only to empty bills (0 payments).");
            }
        }
        
        var updateBillResult = await billRepository.UpdateAsync(mapper.Map<Dal.Models.Bills.Bill>(request));
        updateBillResult.EnsureSuccess();
    }
}