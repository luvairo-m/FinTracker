using MediatR;

namespace FinTracker.Logic.Handlers.Account.UpdateAccount;

public class UpdateAccountCommand : IRequest
{
    public Guid Id { get; set; }

    public string? Title { get; set; }
    
    public decimal? Balance { get; set; }

    public string? Description { get; set; }
    
    public Guid? CurrencyId { get; set; }
}