using MediatR;

namespace FinTracker.Logic.Handlers.Bill.DeleteBill;

internal class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand>
{
    public Task Handle(DeleteBillCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}