using System.Data;
using Microsoft.Data.SqlClient;

namespace FinTracker.Dal.Logic.Connections;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    public async Task<IDbConnection> CreateAsync(bool opened = true)
    {
        var connection = new SqlConnection(this.connectionString);

        if (opened)
        {
            await connection.OpenAsync();
        }

        return connection;
    }
}