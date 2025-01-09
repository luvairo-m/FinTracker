using AutoMapper;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayments;

internal class GetPaymentsCommandHandler : IRequestHandler<GetPaymentsCommand, GetPaymentsModel>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentsCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<GetPaymentsModel> Handle(GetPaymentsCommand request, CancellationToken cancellationToken)
    {
        var gettingPaymentsResult = await _paymentRepository.SearchAsync(new PaymentSearch
        {
            MinAmount = request.MinAmount,
            MaxAmount = request.MaxAmount,
            Types = request.Types,
            MinDate = request.MinDate,
            MaxDate = request.MaxDate,
            Months = request.Months,
            Years = request.Years,
            BillId = request.BillId
        });
        gettingPaymentsResult.EnsureSuccess();

        return new GetPaymentsModel
        {
            Payments = _mapper.Map<ICollection<GetPaymentModel>>(gettingPaymentsResult.Result)
        };
    }
}