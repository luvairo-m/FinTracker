using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.CreateBill;

public class CreateBillCommand : IRequest<CreateBillModel>
{
    public CreateBillCommand(string title, decimal? balance, string description, Guid? currencyId)
    {
        Title = title;
        Balance = (decimal)balance!;
        Description = description;
        CurrencyId = (Guid)currencyId!;
    }

    public string Title { get; set; }

    public decimal? Balance { get; set; }

    public string Description { get; set; }

    public Guid? CurrencyId { get; set; }
}