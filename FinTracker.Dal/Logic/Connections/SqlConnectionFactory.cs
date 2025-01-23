using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace FinTracker.Dal.Logic.Connections;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    public async Task<DbConnection> CreateAsync(bool opened = true)
    {
        var connection = new SqlConnection(this.connectionString);

        if (opened)
        {
            await connection.OpenAsync();
        }

        return connection;
    }
}