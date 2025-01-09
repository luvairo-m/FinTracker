using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.CreateCurrency;

public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, CreateCurrencyModel>
{
    private readonly ICurrencyRepository _currencyRepository;

    public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<CreateCurrencyModel> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var newCurrency = new Dal.Models.Currencies.Currency
        {
            Title = request.Title,
            Sign = request.Sign
        };

        var creatingCurrencyResult = await _currencyRepository.AddAsync(newCurrency);
        
        creatingCurrencyResult.EnsureSuccess();

        return new CreateCurrencyModel { CurrencyId = creatingCurrencyResult.Result };
    }
}