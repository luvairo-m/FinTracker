using AutoMapper;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrencies;

public class GetCurrenciesCommandHandler : IRequestHandler<GetCurrenciesCommand, GetCurrenciesModel>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public GetCurrenciesCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task<GetCurrenciesModel> Handle(GetCurrenciesCommand request, CancellationToken cancellationToken)
    {
        var gettingCategoriesResult = await _currencyRepository.SearchAsync(new CurrencySearch());
        
        gettingCategoriesResult.EnsureSuccess();

        return new GetCurrenciesModel
        {
            Currencies = _mapper.Map<ICollection<GetCurrencyModel>>(gettingCategoriesResult.Result)
        };
    }
}