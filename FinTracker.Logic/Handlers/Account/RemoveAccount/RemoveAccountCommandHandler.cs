using FinTracker.Dal.Repositories.Accounts;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.RemoveAccount;

// ReSharper disable once UnusedType.Global
internal class RemoveAccountCommandHandler : IRequestHandler<RemoveAccountCommand>
{
    private readonly IAccountRepository accountRepository;

    public RemoveAccountCommandHandler(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public async Task Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
    {
        var removeResult = await accountRepository.RemoveAsync(request.AccountId);
        removeResult.EnsureSuccess();
    }
}