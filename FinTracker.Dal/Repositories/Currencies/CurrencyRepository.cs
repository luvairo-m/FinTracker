using FinTracker.Dal.Models.Currencies;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Currencies;

public class CurrencyRepository : 
    RepositoryBase<Currency, CurrencySearch, CurrencyUpdate>,
    ICurrencyRepository
{
    protected override string EntityName => nameof(Currency);
    
    public CurrencyRepository(string connectionString, ILog log) 
        : base(connectionString, log?.ForContext<CurrencyRepository>())
    {
    }
}