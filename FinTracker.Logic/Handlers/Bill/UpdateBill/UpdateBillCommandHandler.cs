using MediatR;

namespace FinTracker.Logic.Handlers.Bill.UpdateBill;

internal class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand>
{
    public Task Handle(UpdateBillCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}