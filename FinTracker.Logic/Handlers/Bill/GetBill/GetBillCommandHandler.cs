using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBill;

internal class GetBillCommandHandler : IRequestHandler<GetBillCommand, GetBillModel>
{
    public Task<GetBillModel> Handle(GetBillCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}