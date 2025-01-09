using AutoMapper;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

internal class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var gettingPaymentsResult = await _paymentRepository.SearchAsync(new PaymentSearch { Id = request.Id });
        gettingPaymentsResult.EnsureSuccess();
        
        var existingPayment = gettingPaymentsResult.Result.FirstOrDefault();
        
        var updatedPayment = _mapper.Map(request, existingPayment);
        
        var updatingCategoryResult = await _paymentRepository.UpdateAsync(updatedPayment);
        updatingCategoryResult.EnsureSuccess();
    }
}