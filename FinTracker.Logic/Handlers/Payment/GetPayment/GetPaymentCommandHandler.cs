using AutoMapper;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayment;

internal class GetPaymentCommandHandler : IRequestHandler<GetPaymentCommand, GetPaymentModel>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<GetPaymentModel> Handle(GetPaymentCommand request, CancellationToken cancellationToken)
    {
        var gettingPaymentsResult = await _paymentRepository.SearchAsync(new PaymentSearch { Id = request.PaymentId });
        gettingPaymentsResult.EnsureSuccess();
        
        var payment = gettingPaymentsResult.Result.FirstOrDefault();
        
        return _mapper.Map<GetPaymentModel>(payment);
    }
}