using MediatR;

namespace FinTracker.Logic.Handlers.Bill.DeleteBill;

public class DeleteBillCommand : IRequest
{
    public DeleteBillCommand(Guid billId)
    {
        BillId = billId;
    }
    
    public Guid BillId { get; set; }
}