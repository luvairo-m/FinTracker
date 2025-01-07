using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrencies;

public class GetCurrenciesCommandHandler : IRequestHandler<GetCurrenciesCommand, GetCurrenciesModel>
{
    public Task<GetCurrenciesModel> Handle(GetCurrenciesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}