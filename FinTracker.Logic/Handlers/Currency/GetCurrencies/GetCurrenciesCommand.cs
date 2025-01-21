using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrencies;

public class GetCurrenciesCommand : IRequest<ICollection<GetCurrencyModel>>
{
    public string TitleSubstring { get; set; }
}