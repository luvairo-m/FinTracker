using MediatR;

namespace FinTracker.Logic.Handlers.Payment.DeletePayment;

internal class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand>
{
    public Task Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}