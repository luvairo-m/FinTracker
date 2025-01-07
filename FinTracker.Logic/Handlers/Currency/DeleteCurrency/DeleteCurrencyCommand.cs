using MediatR;

namespace FinTracker.Logic.Handlers.Currency.DeleteCurrency;

public class DeleteCurrencyCommand : IRequest
{
    public DeleteCurrencyCommand(Guid currencyId)
    {
        CurrencyId = currencyId;
    }
    
    public Guid CurrencyId { get; set; }
}