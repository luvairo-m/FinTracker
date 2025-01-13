using AutoMapper;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayment;

internal class GetPaymentCommandHandler : IRequestHandler<GetPaymentCommand, GetPaymentModel>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetPaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<GetPaymentModel> Handle(GetPaymentCommand request, CancellationToken cancellationToken)
    {
        var gettingPaymentsResult = await paymentRepository.SearchAsync(new PaymentSearch { Id = request.PaymentId });
        gettingPaymentsResult.EnsureSuccess();
        
        var payment = gettingPaymentsResult.Result.FirstOrDefault();
        
        return mapper.Map<GetPaymentModel>(payment);
    }
}