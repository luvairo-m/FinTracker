using FinTracker.Dal.Repositories.Currencies;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.RemoveCurrency;

internal class RemoveCurrencyCommandHandler : IRequestHandler<RemoveCurrencyCommand>
{
    private readonly ICurrencyRepository currencyRepository;

    public RemoveCurrencyCommandHandler(ICurrencyRepository currencyRepository)
    {
        this.currencyRepository = currencyRepository;
    }

    public async Task Handle(RemoveCurrencyCommand request, CancellationToken cancellationToken)
    {
        var deletionCurrencyResult = await currencyRepository.RemoveAsync(request.Id);
        deletionCurrencyResult.EnsureSuccess();
    }
}