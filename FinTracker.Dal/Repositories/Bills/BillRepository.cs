using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Bills;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Bills;

public class BillRepository : RepositoryBase<Bill, BillSearch>, IBillRepository
{
    protected override string EntityName => nameof(Bill);
    
    public BillRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log)
    {
    }
}