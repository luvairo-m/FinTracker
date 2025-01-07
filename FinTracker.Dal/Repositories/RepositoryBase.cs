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
    protected static readonly string KeyColumnName = typeof(TModel).GetKeyColumnName();
    protected static readonly string TableName = typeof(TModel).GetTableName();
    
    private static readonly string EntityName = typeof(TModel).Name;
    
    private readonly ISqlConnectionFactory connectionFactory;
    private readonly ILog log;
    private readonly TimeSpan defaultTimeout = TimeSpan.FromSeconds(5);
    
    protected RepositoryBase(ISqlConnectionFactory connectionFactory, ILog log, TimeSpan? defaultTimeout = null)
    {
        this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        this.log = log ?? throw new ArgumentNullException(nameof(log));
        this.defaultTimeout = defaultTimeout ?? this.defaultTimeout;
    }
    
    public virtual async Task<DbQueryResult<Guid>> AddAsync(TModel model, TimeSpan? timeout = null)
    {
        var columns = string.Join(", ", typeof(TModel).GetColumnNames());
        var parameters = string.Join(", ", typeof(TModel).GetParameterNames(withKeys: false));
        
        var insertScript = @$"INSERT INTO {TableName} ({columns})
                              OUTPUT INSERTED.{KeyColumnName}
                              VALUES (NEWID(), {parameters})";
        
        using var connection = await this.connectionFactory.CreateAsync();
        
        try
        {
            this.log.Info("Trying to add new {0} to database...", EntityName);
            
            var entityId = await connection.ExecuteScalarAsync<Guid>(
                new CommandDefinition(
                    insertScript, 
                    model, 
                    commandTimeout: GetTimeoutSeconds(timeout)));

            this.log.Info("New {0} (Id: {1}) successfully added!", EntityName, entityId);
            
            return DbQueryResult<Guid>.Ok(entityId);
        }
        catch (SqlException sqlException)
        {
            var conflictError = sqlException.Number is 2601 or 2627;

            var errorMessage = conflictError
                ? $"Inserting new {EntityName} failed: unique constraint violated."
                : $"Error while adding new {EntityName} to database.";
            
            this.log.Error(sqlException, errorMessage);
            
            return conflictError 
                ? DbQueryResult<Guid>.Conflict(errorMessage) 
                : DbQueryResult<Guid>.Error(errorMessage);
        }
    }
    
    public virtual async Task<DbQueryResult<ICollection<TModel>>> SearchAsync(
        TSearchModel search, 
        int skip = 0,
        int take = int.MaxValue,
        TimeSpan? timeout = null)
    {
        var selectScript = $@"SELECT {string.Join(", ", typeof(TModel).GetColumnNames())}
                              FROM {TableName}
                              {search.ToWhereExpression()}
                              ORDER BY {KeyColumnName} ASC
                              OFFSET {skip} ROWS
                              FETCH NEXT {take} ROWS ONLY";

        using var connection = await this.connectionFactory.CreateAsync();
                
        try
        { 
            this.log.Info("Searching for {0} entities by filter: {1}...", EntityName, search.ToString());
            
            var foundEntities = (await connection.QueryAsync<TModel>(
                new CommandDefinition(
                    selectScript, 
                    search.ToParametersWithValues(), 
                    commandTimeout: GetTimeoutSeconds(timeout)))).AsList();

            this.log.Info("Search operation completed! Found {0} entities.", foundEntities.Count);
            
            return foundEntities.Count == 0
                ? DbQueryResult<ICollection<TModel>>.NotFound($"No {EntityName} found.")
                : DbQueryResult<ICollection<TModel>>.Ok(foundEntities);
        }
        catch (SqlException sqlException)
        {
            var errorMessage = $"Error while selecting {EntityName} entities from database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult<ICollection<TModel>>.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> UpdateAsync(TModel update, TimeSpan? timeout = null)
    {
        var updateScript = $"UPDATE {TableName} {update.ToSetExpression()} WHERE {KeyColumnName} = @Id";

        using var connection = await this.connectionFactory.CreateAsync();

        try
        { 
            this.log.Info("Trying to update {0} (Id: {1})...", EntityName, update.Id);

            await connection.ExecuteAsync(
                new CommandDefinition(
                    updateScript, 
                    update, 
                    commandTimeout: GetTimeoutSeconds(timeout)));

            this.log.Info("{0} successfully updated!", EntityName);
            
            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            var errorMessage = $"Error while updating {EntityName} in database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null)
    {
        var removeScript = $"DELETE FROM {TableName} WHERE {KeyColumnName} = @Id";

        using var connection = await this.connectionFactory.CreateAsync();
        
        try
        { 
            this.log.Info("Trying to remove {0} (Id: {1}) from database...", EntityName, id);
            
            await connection.ExecuteAsync(
                new CommandDefinition(
                    removeScript, 
                    new { Id = id }, 
                    commandTimeout: GetTimeoutSeconds(timeout)));
            
            this.log.Info("Successfully removed {0} (Id: {1})!", EntityName, id);

            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            var errorMessage = $"Error while removing {EntityName} from database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult.Error(errorMessage);
        }
    }
    
    protected int GetTimeoutSeconds(TimeSpan? timeout)
    {
        return (int)(timeout?.TotalSeconds ?? this.defaultTimeout.TotalSeconds);
    }
}