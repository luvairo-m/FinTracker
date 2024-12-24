using Dapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Utils;
using FinTracker.Dal.Models.Categories;
using Microsoft.Data.SqlClient;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Categories;

public class CategoryRepository : ICategoryRepository
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(7);
    
    private readonly string connectionString;
    private readonly ILog log;

    public CategoryRepository(string connectionString, ILog log)
    {
        this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        this.log = log.ForContext<CategoryRepository>() ?? throw new ArgumentNullException(nameof(log));
    }
    
    public async Task<DbQueryResult<Guid>> AddAsync(Category category, TimeSpan? timeout = null)
    {
        var insertScript = $@"INSERT INTO {typeof(Category).GetTableName()} ({typeof(Category).GetColumnNamesString()})
                              OUTPUT INSERTED.Id
                              VALUES (NEWID(), @Title)";
            
        await using var connection = new SqlConnection(this.connectionString);
        
        try
        {
            this.log.Info("Trying to add new Category to database...");
            
            var categoryId = await connection.ExecuteScalarAsync<Guid>(
                new CommandDefinition(
                    insertScript, 
                    new { category.Title }, 
                    commandTimeout: (int)(timeout?.TotalSeconds ?? DefaultTimeout.TotalSeconds)));

            this.log.Info("New Category (Id: {0}) successfully added!", categoryId);
            
            return DbQueryResult<Guid>.Ok(categoryId);
        }
        catch (SqlException sqlException)
        {
            const string errorMessage = "Error while adding Category to database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult<Guid>.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> RemoveAsync(Guid id, TimeSpan? timeout = null)
    {
        var removeScript = $"DELETE FROM {typeof(Category).GetTableName()} WHERE Id = @Id";
            
        await using var connection = new SqlConnection(this.connectionString);
        
        try
        { 
            this.log.Info("Trying to remove Category (Id: {0}) from database...", id);
            
            await connection.ExecuteAsync(
                new CommandDefinition(
                    removeScript, 
                    new { Id = id }, 
                    commandTimeout: (int)(timeout?.TotalSeconds ?? DefaultTimeout.TotalSeconds)));
            
            this.log.Info("Successfully removed Category (Id: {0})!", id);

            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            const string errorMessage = "Error while removing Category from database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult<ICollection<Category>>> SearchAsync(CategoryFilter filter, TimeSpan? timeout = null)
    {
        var selectScript = $@"SELECT {typeof(Category).GetColumnNamesString()} 
                              FROM {typeof(Category).GetTableName()}
                              {filter.ToWhereExpression()}";
            
        await using var connection = new SqlConnection(this.connectionString);
        
        try
        { 
            this.log.Info("Searching for Categories by filter: {0}", filter.ToFilterDescription());
            
            var categories = (await connection.QueryAsync<Category>(
                new CommandDefinition(
                    selectScript, 
                    parameters: filter.ToParametersWithValues(), 
                    commandTimeout: (int)(timeout?.TotalSeconds ?? DefaultTimeout.TotalSeconds)))).AsList();

            this.log.Info("Search operation completed! Found {0} entities.", categories.Count);
            
            return categories.Count == 0
                ? DbQueryResult<ICollection<Category>>.NotFound("No categories found.")
                : DbQueryResult<ICollection<Category>>.Ok(categories);
        }
        catch (SqlException sqlException)
        {
            const string errorMessage = "Error while selecting Categories from database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult<ICollection<Category>>.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> UpdateAsync(CategoryFilter filter, string newTitle, TimeSpan? timeout = null)
    {
        var updateScript = $"UPDATE {typeof(Category).GetTableName()} SET Title = @Title {filter.ToWhereExpression()}";
            
        await using var connection = new SqlConnection(this.connectionString);

        var filterParameters = filter.ToParametersWithValues();
        filterParameters["Title"] = newTitle;
        
        try
        { 
            this.log.Info("Trying to update Category selected by filter: {0}...", filter.ToFilterDescription());
            
            await connection.ExecuteAsync(
                new CommandDefinition(
                    updateScript, 
                    filterParameters, 
                    commandTimeout: (int)(timeout?.TotalSeconds ?? DefaultTimeout.TotalSeconds)));

            this.log.Info("Category successfully updated!");
            
            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            const string errorMessage = "Error while updating Categories in database.";
            this.log.Error(sqlException, errorMessage);

            return DbQueryResult.Error(errorMessage);
        }
    }
}