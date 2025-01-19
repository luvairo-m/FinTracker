using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Models.Accounts;

namespace FinTracker.Dal.Repositories.Accounts;

/// <summary>
/// Репозиторий для работы со счетами.
/// </summary>
public interface IAccountRepository : IRepository<Account, AccountSearch>
{
}