using FinTracker.Dal.Repositories.Accounts;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.RemoveAccount;

internal class RemoveAccountCommandHandler : IRequestHandler<RemoveAccountCommand>
{
    private readonly IAccountRepository accountRepository;

    public RemoveAccountCommandHandler(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public async Task Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
    {
        var deletionBillResult = await accountRepository.RemoveAsync(request.AccountId);
        deletionBillResult.EnsureSuccess();
    }
}