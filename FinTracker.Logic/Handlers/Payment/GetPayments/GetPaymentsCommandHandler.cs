using AutoMapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayments;

// ReSharper disable once UnusedType.Global
internal class GetPaymentsCommandHandler : IRequestHandler<GetPaymentsCommand, ICollection<GetPaymentModel>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetPaymentsCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<ICollection<GetPaymentModel>> Handle(GetPaymentsCommand request, CancellationToken cancellationToken)
    {
        var getResult = await paymentRepository.SearchAsync(mapper.Map<PaymentSearch>(request));

        if (getResult.Status == DbQueryResultStatus.NotFound)
        {
            return Array.Empty<GetPaymentModel>();
        }
        
        getResult.EnsureSuccess();

        return mapper.Map<ICollection<GetPaymentModel>>(getResult.Result);
    }
}