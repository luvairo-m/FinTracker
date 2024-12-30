using Dapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models;
using Microsoft.Data.SqlClient;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories;

public abstract class RepositoryBase<TModel, TSearchModel, TUpdateModel> 
    where TModel : IEntity
{
    protected abstract string EntityName { get; }
    
    private static readonly string TableName = typeof(TModel).GetTableName();
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);
    
    private readonly string connectionString;
    private readonly ILog log;
    
    protected RepositoryBase(string connectionString, ILog log)
    {
        this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        this.log = log ?? throw new ArgumentNullException(nameof(log));
    }
    
    public async Task<DbQueryResult<Guid>> AddAsync(TModel model, TimeSpan? timeout = null)
    {
        var columns = string.Join(", ", typeof(TModel).GetColumnNames());
        var parameters = string.Join(", ", typeof(TModel).GetParameterNames(includeKeys: false));
        
        var insertScript = @$"INSERT INTO {TableName} ({columns})
                              OUTPUT INSERTED.Id
                              VALUES (NEWID(), {parameters})";
        
        await using var connection = new SqlConnection(this.connectionString);
        
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
            var errorMessage = $"Error while adding new {this.EntityName} to database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult<Guid>.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null)
    {
        var removeScript = $"DELETE FROM {TableName} WHERE Id = @Id";
            
        await using var connection = new SqlConnection(this.connectionString);
        
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

    public async Task<DbQueryResult<ICollection<TModel>>> SearchAsync(TSearchModel searchModel, TimeSpan? timeout = null)
    {
        var selectScript = $@"SELECT {string.Join(", ", typeof(TModel).GetColumnNames())}
                              FROM {TableName}
                              {searchModel.ToWhereExpression()}";
            
        await using var connection = new SqlConnection(this.connectionString);
                
        try
        { 
            this.log.Info("Searching for {0} entities by filter: {1}...", this.EntityName, searchModel.ToString());
            
            var foundEntities = (await connection.QueryAsync<TModel>(
                new CommandDefinition(
                    selectScript, 
                    searchModel.ToParametersWithValues((type, val) => type == typeof(string) ? $"%{val}%" : val), 
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

    public async Task<DbQueryResult> UpdateAsync(Guid id, TUpdateModel updateModel, TimeSpan? timeout = null)
    {
        var updateScript = $"UPDATE {TableName} {updateModel.ToSetExpression()} WHERE Id = @Id";
        
        await using var connection = new SqlConnection(this.connectionString);

        try
        { 
            this.log.Info("Trying to update {0} (Id: {1})...", this.EntityName, id);

            var updateParams = updateModel.ToParametersWithValues();
            updateParams.Add("Id", id);
            
            await connection.ExecuteAsync(
                new CommandDefinition(
                    updateScript, 
                    updateParams, 
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