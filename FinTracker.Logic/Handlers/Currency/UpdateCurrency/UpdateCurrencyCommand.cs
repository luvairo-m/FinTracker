using MediatR;

namespace FinTracker.Logic.Handlers.Currency.UpdateCurrency;

public class UpdateCurrencyCommand : IRequest
{
    public UpdateCurrencyCommand(Guid id, string title, string sign)
    {
        Id = id;
        Title = title;
        Sign = sign;
    }
    
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Sign { get; set; }
}