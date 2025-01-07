using MediatR;

namespace FinTracker.Logic.Handlers.Currency.UpdateCurrency;

public class UpdateCurrencyCommand : IRequest
{
    public UpdateCurrencyCommand(Guid currencyId, string title, string sign)
    {
        CurrencyId = currencyId;
        Title = title;
        Sign = sign;
    }
    
    public Guid CurrencyId { get; set; }

    public string Title { get; set; }

    public string Sign { get; set; }
}