using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrency;

public class GetCurrencyCommandHandler : IRequestHandler<GetCurrencyCommand, GetCurrencyModel>
{
    public Task<GetCurrencyModel> Handle(GetCurrencyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}