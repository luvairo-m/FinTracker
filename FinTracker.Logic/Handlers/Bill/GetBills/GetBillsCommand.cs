using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBills;

public class GetBillsCommand : IRequest<ICollection<GetBillModel>>
{
    public Guid? Id { get; set; }

    public string Title { get; set; }
    
    public Guid? CurrencyId { get; set; }
}