using AutoMapper;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Dal.Repositories.Currencies;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.UpdateCurrency;

public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;
    
    public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var updatedCurrency = _mapper.Map<Dal.Models.Currencies.Currency>(request);
        
        var updatingCurrencyResult = await _currencyRepository.UpdateAsync(updatedCurrency);
        updatingCurrencyResult.EnsureSuccess();
    }
}