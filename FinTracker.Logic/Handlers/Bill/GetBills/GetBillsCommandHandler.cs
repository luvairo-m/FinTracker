using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBills;

internal class GetBillsCommandHandler : IRequestHandler<GetBillsCommand, GetBillsModel>
{
    public Task<GetBillsModel> Handle(GetBillsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}