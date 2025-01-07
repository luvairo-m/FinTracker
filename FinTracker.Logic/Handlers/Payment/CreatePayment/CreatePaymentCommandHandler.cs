using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

internal class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentModel>
{
    public Task<CreatePaymentModel> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}