using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Models.Bills;

namespace FinTracker.Dal.Repositories.Bills;

/// <summary>
/// Репозиторий для работы со счетами.
/// </summary>
public interface IBillRepository : IRepository<Bill, BillSearch>
{
}