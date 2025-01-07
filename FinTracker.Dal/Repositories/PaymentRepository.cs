using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Models.Payments;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories;

public class PaymentRepository : RepositoryBase<Payment, PaymentSearch>
{
    public PaymentRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log?.ForContext<PaymentRepository>())
    {
    }
}