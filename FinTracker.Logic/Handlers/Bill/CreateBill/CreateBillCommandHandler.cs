using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.CreateBill;

internal class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, CreateBillModel>
{
    public Task<CreateBillModel> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}