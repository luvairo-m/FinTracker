using AutoMapper;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrency;

// ReSharper disable once UnusedType.Global
internal class GetCurrencyCommandHandler : IRequestHandler<GetCurrencyCommand, GetCurrencyModel>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly IMapper mapper;

    public GetCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        this.currencyRepository = currencyRepository;
        this.mapper = mapper;
    }

    public async Task<GetCurrencyModel> Handle(GetCurrencyCommand request, CancellationToken cancellationToken)
    {
        var gettingCurrenciesResult = await currencyRepository.SearchAsync(new CurrencySearch { Id = request.CurrencyId });
        gettingCurrenciesResult.EnsureSuccess();

        var currency = gettingCurrenciesResult.Result.FirstOrDefault();
        
        return mapper.Map<GetCurrencyModel>(currency);
    }
}