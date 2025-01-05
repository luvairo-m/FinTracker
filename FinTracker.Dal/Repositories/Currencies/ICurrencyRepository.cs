using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Currencies;

namespace FinTracker.Dal.Repositories.Currencies;

/// <summary>
/// Репозиторий для работы с валютами.
/// </summary>
public interface ICurrencyRepository
{
    /// <summary>
    /// Добавить новую валюту.
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<Guid>> AddAsync(Currency currency, TimeSpan? timeout = null);

    /// <summary>
    /// Поиск по валютам.
    /// </summary>
    /// <param name="search"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult<ICollection<Currency>>> SearchAsync(CurrencySearch search, TimeSpan? timeout = null);

    /// <summary>
    /// Удалить валюту.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null);

    /// <summary>
    /// Обновить валюту по идентификатору.
    /// </summary>
    /// <param name="update"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<DbQueryResult> UpdateAsync(Currency update, TimeSpan? timeout = null);
}