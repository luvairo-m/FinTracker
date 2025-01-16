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
        var gettingPaymentsResult = await paymentRepository.SearchAsync(mapper.Map<PaymentSearch>(request));
        gettingPaymentsResult.EnsureSuccess();

        return new GetPaymentsModel
        {
            Payments = mapper.Map<ICollection<GetPaymentModel>>(gettingPaymentsResult.Result)
        };
    }
}