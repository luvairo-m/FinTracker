using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Bills;

namespace FinTracker.Dal.Repositories.Bills;

/// <summary>
/// Репозиторий для работы со счетами.
/// </summary>
public interface IBillRepository
{
    /// <summary>
    /// Добавить новый счет.
    /// </summary>
    /// <param name="bill"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<Guid>> AddAsync(Bill bill, TimeSpan? timeout = null);

    /// <summary>
    /// Поиск по счетам.
    /// </summary>
    /// <param name="search"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<ICollection<Bill>>> SearchAsync(BillSearch search, TimeSpan? timeout = null);

    /// <summary>
    /// Удалить счет.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null);

    /// <summary>
    /// Обновить счет по идентификатору.
    /// </summary>
    /// <param name="update"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> UpdateAsync(Bill update, TimeSpan? timeout = null);
}