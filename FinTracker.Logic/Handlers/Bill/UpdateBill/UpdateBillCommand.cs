using MediatR;

namespace FinTracker.Logic.Handlers.Bill.UpdateBill;

public class UpdateBillCommand : IRequest
{
    public UpdateBillCommand(Guid id, string title, string description, Guid? currencyId)
    {
        Id = id;
        Title = title;
        Description = description;
        CurrencyId = currencyId;
    }
    
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public Guid? CurrencyId { get; set; }
}