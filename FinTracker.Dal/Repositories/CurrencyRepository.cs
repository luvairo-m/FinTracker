using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Currencies;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories;

public class CurrencyRepository : RepositoryBase<Currency, CurrencySearch>
{
    public CurrencyRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log?.ForContext<CurrencyRepository>())
    {
    }
}