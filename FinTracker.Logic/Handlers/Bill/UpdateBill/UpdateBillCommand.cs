using MediatR;

namespace FinTracker.Logic.Handlers.Bill.UpdateBill;

public class UpdateBillCommand : IRequest
{
    public UpdateBillCommand(Guid billId, string title, decimal? balance, string description, Guid? currencyId)
    {
        BillId = billId;
        Title = title;
        Balance = balance;
        Description = description;
        CurrencyId = currencyId;
    }
    
    public Guid BillId { get; set; }

    public string Title { get; set; }

    public decimal? Balance { get; set; }

    public string Description { get; set; }
    
    public Guid? CurrencyId { get; set; }
}