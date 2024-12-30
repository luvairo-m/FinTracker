using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

internal class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
{
    public Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}