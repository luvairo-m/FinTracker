using FinTracker.Dal.Models.Abstractions;
using FinTracker.Dal.Models.Currencies;

namespace FinTracker.Dal.Repositories.Currencies;

/// <summary>
/// Репозиторий для работы с валютами.
/// </summary>
public interface ICurrencyRepository : IRepository<Currency, CurrencySearch>
{
}