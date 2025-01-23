using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Accounts;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Accounts;

public class AccountRepository : RepositoryBase<Account, AccountSearch>, IAccountRepository
{
    public AccountRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log?.ForContext<AccountRepository>())
    {
    }
}