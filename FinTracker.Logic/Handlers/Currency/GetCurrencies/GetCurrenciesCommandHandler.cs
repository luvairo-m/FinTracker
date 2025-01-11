using AutoMapper;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrencies;

public class GetCurrenciesCommandHandler : IRequestHandler<GetCurrenciesCommand, GetCurrenciesModel>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly IMapper mapper;

    public GetCurrenciesCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        this.currencyRepository = currencyRepository;
        this.mapper = mapper;
    }

    public async Task<GetCurrenciesModel> Handle(GetCurrenciesCommand request, CancellationToken cancellationToken)
    {
        var gettingCategoriesResult = await currencyRepository.SearchAsync(new CurrencySearch());
        gettingCategoriesResult.EnsureSuccess();

        return new GetCurrenciesModel
        {
            Currencies = mapper.Map<ICollection<GetCurrencyModel>>(gettingCategoriesResult.Result)
        };
    }
}