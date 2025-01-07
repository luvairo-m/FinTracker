using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrency;

public class GetCurrencyCommand : IRequest<GetCurrencyModel>
{
    public GetCurrencyCommand(Guid currencyId)
    {
        CurrencyId = currencyId;
    }
    
    public Guid CurrencyId { get; set; }
}