using System.Data;
using System.Text;
using Dapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models.Payments;
using FinTracker.Infra.Extensions;
using Microsoft.Data.SqlClient;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal.Repositories.Payments;

public class PaymentRepository : RepositoryBase<Payment, PaymentSearch>, IPaymentRepository
{
    private readonly ISqlConnectionFactory connectionFactory;
    private readonly ILog log;
    
    public PaymentRepository(ISqlConnectionFactory connectionFactory, ILog log) 
        : base(connectionFactory, log?.ForContext<PaymentRepository>())
    {
        this.connectionFactory = connectionFactory;
        this.log = log;
    }

    public override async Task<DbQueryResult<Guid>> AddAsync(Payment model, TimeSpan? timeout = null)
    {
        var insertParameters = model.ToParametersWithValues();
        var (categoryInsertScript, categoryParameters) = model.Categories?.Count > 0
            ? CreateCategoriesInsertScript(model.Categories)
            : (null, null);

        // Параметр @Id уже определен в sql-скрипте.
        insertParameters.Remove("Id");

        if (categoryParameters != null)
        {
            foreach (var (paramName, paramValue) in categoryParameters)
            {
                insertParameters.Add(paramName, paramValue);
            }
        }
        
        var insertScript = $@"DECLARE @Id uniqueidentifier;
                              SET @Id = NEWID();

                              INSERT INTO {TableName} ({typeof(Payment).GetColumnNames().AsCommaSeparated()})
                              OUTPUT INSERTED.{KeyColumnName}
                              VALUES (@Id, {typeof(Payment).GetParameterNames(withKeys: false).AsCommaSeparated()})
                              
                              {categoryInsertScript}";

        await using var connection = await this.connectionFactory.CreateAsync();
        await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        
        try
        {
            this.log.Info("Trying to add new Payment to database...");
            
            var entityId = await connection.ExecuteScalarAsync<Guid>(
                new CommandDefinition(
                    insertScript, 
                    insertParameters, 
                    transaction: transaction,
                    commandTimeout: GetTimeoutSeconds(timeout)));

            await transaction.CommitAsync();

            this.log.Info("New Payment (Id: {1}) successfully added!", entityId);
            
            return DbQueryResult<Guid>.Ok(entityId);
        }
        catch (SqlException sqlException)
        {
            const string errorMessage = "Error while adding new Payment to database.";
            this.log.Error(sqlException, errorMessage);
            
            await transaction.RollbackAsync();
            
            return DbQueryResult<Guid>.Error(errorMessage);
        }
    }

    public override async Task<DbQueryResult<ICollection<Payment>>> SearchAsync(
        PaymentSearch search, 
        int skip = 0, 
        int take = int.MaxValue,
        TimeSpan? timeout = null)
    {
        var selectScript = CreateSelectScript(search, skip, take);
        var selectParameters = search.ToParametersWithValues();
        
        selectParameters.Add("CategoryIds", search.Categories);

        await using var connection = await this.connectionFactory.CreateAsync();
                
        try
        { 
            this.log.Info("Searching for Payments by filter: {1}...", search.ToString());
            
            var foundPayments = (await connection.QueryAsync<Payment>(
                new CommandDefinition(
                    selectScript, 
                    selectParameters, 
                    commandTimeout: GetTimeoutSeconds(timeout)))).AsList();

            if (foundPayments.Count > 0)
            {
                var paymentToCategories = foundPayments.ToDictionary(pay => pay.Id, _ => new List<Guid>());
                
                await connection.QueryAsync<Guid, Guid, Guid>(
                    "SELECT PaymentId, CategoryId FROM [dbo].PaymentCategory WHERE PaymentId IN @PaymentIds",
                    (paymentId, categoryId) =>
                    { 
                        paymentToCategories[paymentId].Add(categoryId);
                        return paymentId;
                    },
                    new { PaymentIds = foundPayments.Select(pay => pay.Id) },
                    splitOn: "CategoryId",
                    commandTimeout: GetTimeoutSeconds(timeout));

                foreach (var payment in foundPayments)
                {
                    payment.Categories = paymentToCategories[payment.Id];
                }
            }
    
            this.log.Info("Search operation completed! Found {0} Payments.", foundPayments.Count);
            
            return foundPayments.Count == 0
                ? DbQueryResult<ICollection<Payment>>.NotFound("No Payments found.")
                : DbQueryResult<ICollection<Payment>>.Ok(foundPayments);
        }
        catch (SqlException sqlException)
        {
            const string errorMessage = "Error while selecting Payment entities from database.";
            this.log.Error(sqlException, errorMessage);
    
            return DbQueryResult<ICollection<Payment>>.Error(errorMessage);
        }
    }

    public async Task<DbQueryResult> UpdateCategoriesAsync(
        Guid paymentId, 
        ICollection<Guid> addCategories = null, 
        ICollection<Guid> removeCategories = null,
        TimeSpan? timeout = null)
    {
        if (addCategories.IsNullOrEmpty() && removeCategories.IsNullOrEmpty())
        {
            return DbQueryResult.Ok();
        }

        var (insertPart, updateParameters) = addCategories?.Count > 0
            ? CreateCategoriesInsertScript(addCategories)
            : (string.Empty, new Dictionary<string, object>());
        
        updateParameters.Add("Id", paymentId);
        updateParameters.Add("RemoveCategories", removeCategories);

        var removePart = removeCategories?.Count > 0
            ? @"DELETE FROM [dbo].PaymentCategory
                WHERE PaymentId = @Id AND CategoryId IN @RemoveCategories"
            : string.Empty;

        await using var connection = await this.connectionFactory.CreateAsync();
        await using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
        
        try
        {
            this.log.Info("Trying to update Payment (Id: {0}) categories...", paymentId);
            
            var entityId = await connection.ExecuteAsync(
                new CommandDefinition(
                    $"{insertPart}\n{removePart}", 
                    updateParameters,
                    transaction: transaction,
                    commandTimeout: GetTimeoutSeconds(timeout)));
            
            await transaction.CommitAsync();

            this.log.Info("Payment's (Id: {0}) categories were successfully updated!",  paymentId);
            
            return DbQueryResult.Ok();
        }
        catch (SqlException sqlException)
        {
            const string errorMessage = "Error while updating categories.";
            this.log.Error(sqlException, errorMessage);
            
            await transaction.RollbackAsync();
            
            return DbQueryResult.Error(errorMessage);
        }
    }

    private static string CreateSelectScript(PaymentSearch search, int skip, int take)
    {
        var categoriesCondition = search.Categories.IsNullOrEmpty()
            ? string.Empty
            : @"AND EXISTS (
                    SELECT * FROM [dbo].PaymentCategory
                    WHERE PaymentId = @Id AND CategoryId IN @CategoryIds
                )";
        
        var selectScript = @$"SELECT {typeof(Payment).GetColumnNames().AsCommaSeparated()}
                              FROM {TableName}
                              {search.ToWhereExpression()}
                              {categoriesCondition}
                              ORDER BY {KeyColumnName} ASC
                              OFFSET {skip} ROWS
                              FETCH NEXT {take} ROWS ONLY";

        return selectScript;
    }

    private static (string script, Dictionary<string, object> parameters) CreateCategoriesInsertScript(ICollection<Guid> categoryIds)
    {
        const string template = "categoryId_";
        
        var scriptBuilder = new StringBuilder();
        var parameters = new Dictionary<string, object>(categoryIds.Count);
        
        var index = 0;
        
        scriptBuilder.AppendLine("INSERT INTO [dbo].PaymentCategory (PaymentId, CategoryId) VALUES");
        
        foreach (var id in categoryIds)
        {
            var paramName = template + index;
            var suffix = index == categoryIds.Count - 1 ? string.Empty : ",";
            
            scriptBuilder.AppendLine($"(@Id, @{paramName}){suffix}");
            parameters.Add(paramName, id);
            
            index++;
        }
        
        return (scriptBuilder.ToString(), parameters);
    }
}