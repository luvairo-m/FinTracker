using System.Data.Common;

namespace FinTracker.Dal.Logic.Connections;

public interface ISqlConnectionFactory
{
    Task<DbConnection> CreateAsync(bool opened = true);
}