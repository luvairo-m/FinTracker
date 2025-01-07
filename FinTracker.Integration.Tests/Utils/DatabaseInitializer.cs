using System.Text.RegularExpressions;
using Dapper;
using FinTracker.Dal.Logic;
using Microsoft.Data.SqlClient;

namespace FinTracker.Integration.Tests.Utils;

public class DatabaseInitializer
{
    private readonly string connectionString;
        
    private readonly string[] createTablesPaths;
    private readonly string[] dropTablesPath;

    public DatabaseInitializer(string connectionString, string[] createTablesPaths, string[] dropTablesPath)
    {
        this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            
        if (createTablesPaths == null || createTablesPaths.Length == 0)
        {
            throw new ArgumentNullException(nameof(createTablesPaths));
        }

        if (dropTablesPath == null || dropTablesPath.Length == 0)
        {
            throw new ArgumentNullException(nameof(dropTablesPath));
        }
            
        Array.Sort(createTablesPaths);
        Array.Sort(dropTablesPath);

        this.createTablesPaths = createTablesPaths;
        this.dropTablesPath = dropTablesPath;
    }

    public async Task<DbQueryResult> CreateTablesAsync()
    {
        try
        {
            await using var connection = new SqlConnection(this.connectionString);
                
            var scripts = new List<string>();
                
            foreach (var scriptPath in createTablesPaths)
            {
                scripts.AddRange(SplitSqlStatements(await File.ReadAllTextAsync(scriptPath)));
            }
                
            foreach (var script in scripts)
            {
                await connection.ExecuteAsync(script);
            }
                
            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            return DbQueryResult.Error($"Can't create database(s) due to error: {sqlException.Message}.");
        }
    }

    public async Task<DbQueryResult> DropTablesAsync()
    {
        try
        {
            await using var connection = new SqlConnection(this.connectionString);
                
            var scripts = new List<string>();
                
            foreach (var scriptPath in dropTablesPath)
            {
                scripts.AddRange(SplitSqlStatements(await File.ReadAllTextAsync(scriptPath)));
            }

            foreach (var script in scripts)
            {
                await connection.ExecuteAsync(script);
            }
                
            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            return DbQueryResult.Error($"Can't drop database(s) due to error: {sqlException}.");
        }
    }

    private static IEnumerable<string> SplitSqlStatements(string sqlScript)
    {
        var statements = Regex.Split(
            sqlScript,
            @"^[\t\r\n]*GO[\t\r\n]*\d*[\t\r\n]*(?:--.*)?$",
            RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

        return statements
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim(' ', '\r', '\n'));
    }
}