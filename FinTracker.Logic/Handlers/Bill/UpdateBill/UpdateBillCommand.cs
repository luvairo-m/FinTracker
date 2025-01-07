using MediatR;

namespace FinTracker.Logic.Handlers.Bill.UpdateBill;

public class UpdateBillCommand : IRequest
{
    public UpdateBillCommand(Guid billId, string title, decimal amount)
    {
        BillId = billId;
        Title = title;
        Amount = amount;
    }
    
    public Guid BillId { get; set; }

    public string Title { get; set; }

    public decimal Amount { get; set; }
}