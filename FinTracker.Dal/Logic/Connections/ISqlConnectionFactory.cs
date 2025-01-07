using System.Data;

namespace FinTracker.Dal.Logic.Connections;

public interface ISqlConnectionFactory
{
    Task<IDbConnection> CreateAsync(bool opened = true);
}