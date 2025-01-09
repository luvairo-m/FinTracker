using AutoMapper;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.GetCurrency;

public class GetCurrencyCommandHandler : IRequestHandler<GetCurrencyCommand, GetCurrencyModel>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public GetCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task<GetCurrencyModel> Handle(GetCurrencyCommand request, CancellationToken cancellationToken)
    {
        var gettingCurrenciesResult =
            await _currencyRepository.SearchAsync(new CurrencySearch { Id = request.CurrencyId });
        
        gettingCurrenciesResult.EnsureSuccess();

        var currency = gettingCurrenciesResult.Result.FirstOrDefault();
        
        return _mapper.Map<GetCurrencyModel>(currency);
    }
}