using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Bills;

namespace FinTracker.Dal.Repositories.Accounts;

/// <summary>
/// Репозиторий для работы со счетами.
/// </summary>
public interface IAccountRepository : IRepository<Account, AccountSearch>
{
}