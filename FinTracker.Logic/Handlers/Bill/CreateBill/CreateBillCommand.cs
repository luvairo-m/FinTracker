using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.CreateBill;

public class CreateBillCommand : IRequest<CreateBillModel>
{
    public CreateBillCommand(string title, decimal amount)
    {
        Title = title;
        Amount = amount;
    }

    public string Title { get; set; }

    public decimal Amount { get; set; }
}