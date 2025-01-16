using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrency;

public class GetCurrencyCommand : IRequest<GetCurrencyModel>
{
    public Guid CurrencyId { get; set; }
}