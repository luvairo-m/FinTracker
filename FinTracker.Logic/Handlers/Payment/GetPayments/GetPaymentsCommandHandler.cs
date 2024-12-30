using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayments;

internal class GetPaymentsCommandHandler : IRequestHandler<GetPaymentsCommand, GetPaymentsModel>
{
    public Task<GetPaymentsModel> Handle(GetPaymentsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}