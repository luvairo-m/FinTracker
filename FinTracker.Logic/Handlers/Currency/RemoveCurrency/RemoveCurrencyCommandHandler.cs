using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Currencies;
using MediatR;

namespace FinTracker.Logic.Handlers.Currency.RemoveCurrency;

// ReSharper disable once UnusedType.Global
internal class RemoveCurrencyCommandHandler : IRequestHandler<RemoveCurrencyCommand>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly IAccountRepository accountRepository;

    public RemoveCurrencyCommandHandler(ICurrencyRepository currencyRepository, IAccountRepository accountRepository)
    {
        this.currencyRepository = currencyRepository;
        this.accountRepository = accountRepository;
    }

    public async Task Handle(RemoveCurrencyCommand request, CancellationToken cancellationToken)
    {
        var searchAccountsResult = await this.accountRepository.SearchAsync(new AccountSearch { CurrencyId = request.Id });

        if (searchAccountsResult.Status != DbQueryResultStatus.NotFound)
        {
            searchAccountsResult.EnsureSuccess();
        }
        
        if (searchAccountsResult.Status == DbQueryResultStatus.Ok)
        {
            throw new ForbiddenOperation($"Currency is used in accounts: [ {string.Join(", ", searchAccountsResult.Result.Select(acc => acc.Id))} ].");
        }
        
        (await currencyRepository.RemoveAsync(request.Id)).EnsureSuccess();
    }
}