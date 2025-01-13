using FinTracker.Dal.Repositories.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.DeletePayment;

internal class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand>
{
    private readonly IPaymentRepository paymentRepository;

    public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var deletionPaymentResult = await paymentRepository.RemoveAsync(request.PaymentId);
        deletionPaymentResult.EnsureSuccess();
    }
}