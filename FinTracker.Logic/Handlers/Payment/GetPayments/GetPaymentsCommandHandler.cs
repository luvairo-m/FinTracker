using AutoMapper;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayments;

internal class GetPaymentsCommandHandler : IRequestHandler<GetPaymentsCommand, GetPaymentsModel>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetPaymentsCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<GetPaymentsModel> Handle(GetPaymentsCommand request, CancellationToken cancellationToken)
    {
        var gettingPaymentsResult = await paymentRepository.SearchAsync(new PaymentSearch
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
            Payments = mapper.Map<ICollection<GetPaymentModel>>(gettingPaymentsResult.Result)
        };
    }
}