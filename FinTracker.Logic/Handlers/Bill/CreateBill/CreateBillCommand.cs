using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.CreateBill;

public class CreateBillCommand : IRequest<CreateBillModel>
{
    public CreateBillCommand(string title, decimal balance, string description)
    {
        Title = title;
        Balance = balance;
        Description = description;
    }

    public string Title { get; set; }

    public decimal Balance { get; set; }

    public string Description { get; set; }
}