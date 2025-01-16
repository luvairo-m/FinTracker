using AutoMapper;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Logic.Models.Currency;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.CreateCurrency;

internal class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, CreateCurrencyModel>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly IMapper mapper;

    public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        this.currencyRepository = currencyRepository;
        this.mapper = mapper;
    }

    public async Task<CreateCurrencyModel> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var newCurrency = mapper.Map<Dal.Models.Currencies.Currency>(request);

        var creatingCurrencyResult = await currencyRepository.AddAsync(newCurrency);
        creatingCurrencyResult.EnsureSuccess();

        return new CreateCurrencyModel { CurrencyId = creatingCurrencyResult.Result };
    }
}