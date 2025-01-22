using AutoMapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrencies;

// ReSharper disable once UnusedType.Global
internal class GetCurrenciesCommandHandler : IRequestHandler<GetCurrenciesCommand, ICollection<GetCurrencyModel>>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly IMapper mapper;

    public GetCurrenciesCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        this.currencyRepository = currencyRepository;
        this.mapper = mapper;
    }

    public async Task<ICollection<GetCurrencyModel>> Handle(GetCurrenciesCommand request, CancellationToken cancellationToken)
    {
        var getResult = await currencyRepository.SearchAsync(mapper.Map<CurrencySearch>(request));

        if (getResult.Status == DbQueryResultStatus.NotFound)
        {
            return Array.Empty<GetCurrencyModel>();
        }
        
        getResult.EnsureSuccess();

        return mapper.Map<ICollection<GetCurrencyModel>>(getResult.Result);
    }
}