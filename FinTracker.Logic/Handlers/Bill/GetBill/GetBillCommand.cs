using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBill;

public class GetBillCommand : IRequest<GetBillModel>
{
    public GetBillCommand(Guid billId)
    {
        BillId = billId;
    }
    
    public Guid BillId { get; set; }
}