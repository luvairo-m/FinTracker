using AutoMapper;
using FinTracker.Dal.Repositories.Currencies;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.UpdateCurrency;

// ReSharper disable once UnusedType.Global
internal class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly IMapper mapper;
    
    public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        this.currencyRepository = currencyRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var updatedCurrency = mapper.Map<Dal.Models.Currencies.Currency>(request);
        
        var updatingCurrencyResult = await currencyRepository.UpdateAsync(updatedCurrency);
        updatingCurrencyResult.EnsureSuccess();
    }
}