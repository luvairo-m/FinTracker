using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Bills;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories;

public class BillRepository : RepositoryBase<Bill, BillSearch>
{
    public BillRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log?.ForContext<BillRepository>())
    {
    }
}