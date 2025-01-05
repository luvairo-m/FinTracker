using Dapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models.Abstractions;
using Microsoft.Data.SqlClient;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories;

public abstract class RepositoryBase<TModel, TSearchModel> 
    where TModel : IEntity
{
    protected abstract string EntityName { get; }
    
    private static readonly string TableName = typeof(TModel).GetTableName();
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);
    
    private readonly ISqlConnectionFactory connectionFactory;
    private readonly ILog log;
    
    protected RepositoryBase(ISqlConnectionFactory connectionFactory, ILog log)
    {
        this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        this.log = log ?? throw new ArgumentNullException(nameof(log));
    }
    
    public async Task<DbQueryResult<Guid>> AddAsync(TModel model, TimeSpan? timeout = null)
    {
        var columns = string.Join(", ", typeof(TModel).GetColumnNames());
        var parameters = string.Join(", ", typeof(TModel).GetParameterNames(withKeys: false));
        
        var insertScript = @$"INSERT INTO {TableName} ({columns})
                              OUTPUT INSERTED.Id
                              VALUES (NEWID(), {parameters})";

        using var connection = await this.connectionFactory.CreateAsync();
        
        try
        {
            this.log.Info("Trying to add new {0} to database...", this.EntityName);
            
            var entityId = await connection.ExecuteScalarAsync<Guid>(
                new CommandDefinition(
                    insertScript, 
                    model, 
                    commandTimeout: GetTimeoutSeconds(timeout)));

            this.log.Info("New {0} (Id: {1}) successfully added!", this.EntityName, entityId);
            
            return DbQueryResult<Guid>.Ok(entityId);
        }
        catch (SqlException sqlException)
        {
            // Codes are MSSQL specific.
            var conflictError = sqlException.Number is 2601 or 2627;

            var errorMessage = conflictError
                ? $"Inserting new {this.EntityName} failed: unique constraint violated."
                : $"Error while adding new {this.EntityName} to database.";
            
            this.log.Error(sqlException, errorMessage);
            
            return conflictError 
                ? DbQueryResult<Guid>.Conflict(errorMessage) 
                : DbQueryResult<Guid>.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null)
    {
        var removeScript = $"DELETE FROM {TableName} WHERE Id = @Id";

        using var connection = await this.connectionFactory.CreateAsync();
        
        try
        { 
            this.log.Info("Trying to remove {0} (Id: {1}) from database...", this.EntityName, id);
            
            await connection.ExecuteAsync(
                new CommandDefinition(
                    removeScript, 
                    new { Id = id }, 
                    commandTimeout: GetTimeoutSeconds(timeout)));
            
            this.log.Info("Successfully removed {0} (Id: {1})!", this.EntityName, id);

            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            var errorMessage = $"Error while removing {this.EntityName} from database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult<ICollection<TModel>>> SearchAsync(TSearchModel search, TimeSpan? timeout = null)
    {
        var selectScript = $@"SELECT {string.Join(", ", typeof(TModel).GetColumnNames())}
                              FROM {TableName}
                              {search.ToWhereExpression()}";

        using var connection = await this.connectionFactory.CreateAsync();
                
        try
        { 
            this.log.Info("Searching for {0} entities by filter: {1}...", this.EntityName, search.ToString());
            
            var foundEntities = (await connection.QueryAsync<TModel>(
                new CommandDefinition(
                    selectScript, 
                    search.ToParametersWithValues(), 
                    commandTimeout: GetTimeoutSeconds(timeout)))).AsList();

            this.log.Info("Search operation completed! Found {0} entities.", foundEntities.Count);
            
            return foundEntities.Count == 0
                ? DbQueryResult<ICollection<TModel>>.NotFound($"No {this.EntityName} found.")
                : DbQueryResult<ICollection<TModel>>.Ok(foundEntities);
        }
        catch (SqlException sqlException)
        {
            var errorMessage = $"Error while selecting {this.EntityName} entities from database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult<ICollection<TModel>>.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> UpdateAsync(TModel update, TimeSpan? timeout = null)
    {
        var updateScript = $"UPDATE {TableName} {update.ToSetExpression()} WHERE Id = @Id";

        using var connection = await this.connectionFactory.CreateAsync();

        try
        { 
            this.log.Info("Trying to update {0} (Id: {1})...", this.EntityName, update.Id);

            await connection.ExecuteAsync(
                new CommandDefinition(
                    updateScript, 
                    update, 
                    commandTimeout: GetTimeoutSeconds(timeout)));

            this.log.Info("{0} successfully updated!", this.EntityName);
            
            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            var errorMessage = $"Error while updating {this.EntityName} in database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult.Error(errorMessage);
        }
    }
    
    private static int GetTimeoutSeconds(TimeSpan? timeout)
    {
        return (int)(timeout?.TotalSeconds ?? DefaultTimeout.TotalSeconds);
    }
}