using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Currencies;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Currencies;

public class CurrencyRepository : RepositoryBase<Currency, CurrencySearch>, ICurrencyRepository
{
    protected override string EntityName => nameof(Currency);
    
    public CurrencyRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log?.ForContext<CurrencyRepository>())
    {
    }
}