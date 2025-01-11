using FinTracker.Dal.Repositories.Currencies;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.DeleteCurrency;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
{
    private readonly ICurrencyRepository currencyRepository;

    public DeleteCurrencyCommandHandler(ICurrencyRepository currencyRepository)
    {
        this.currencyRepository = currencyRepository;
    }

    public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var deletionCurrencyResult = await currencyRepository.RemoveAsync(request.CurrencyId);
        deletionCurrencyResult.EnsureSuccess();
    }
}