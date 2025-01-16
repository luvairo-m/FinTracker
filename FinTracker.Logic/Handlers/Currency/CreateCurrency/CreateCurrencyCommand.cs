using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.CreateCurrency;

public class CreateCurrencyCommand : IRequest<CreateCurrencyModel>
{
    public string Title { get; set; }

    public string Sign { get; set; }
}