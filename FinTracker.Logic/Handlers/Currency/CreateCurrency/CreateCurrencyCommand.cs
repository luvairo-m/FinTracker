using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.CreateCurrency;

public class CreateCurrencyCommand : IRequest<CreateCurrencyModel>
{
    public CreateCurrencyCommand(string title, string sign)
    {
        Title = title;
        Sign = sign;
    }
    
    public string Title { get; set; }

    public string Sign { get; set; }
}