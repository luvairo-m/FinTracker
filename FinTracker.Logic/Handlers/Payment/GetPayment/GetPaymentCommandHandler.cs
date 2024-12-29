using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayment;

internal class GetPaymentCommandHandler : IRequestHandler<GetPaymentCommand, GetPaymentModel>
{
    public Task<GetPaymentModel> Handle(GetPaymentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}