using FinTracker.Dal.Logic.Exceptions;

namespace FinTracker.Dal.Logic;

#pragma warning disable SA1402 // File may only contain a single class
public enum DbQueryResultStatus
{
    Ok,
    Error,
    NotFound,
    Conflict
}

public class DbQueryResult
{
    public DbQueryResultStatus Status { get; }
    
    public string ErrorMessage { get; }
    
    private DbQueryResult(DbQueryResultStatus status, string errorMessage = null)
    {
        this.Status = status;
        this.ErrorMessage = errorMessage;
    }
    
    public void EnsureSuccess()
    {
        if (this.Status != DbQueryResultStatus.Ok)
        {
            throw new DatabaseQueryException(this.ErrorMessage);
        }
    }

    public static DbQueryResult Ok()
    {
        return new DbQueryResult(DbQueryResultStatus.Ok);
    }
    
    public static DbQueryResult Error(string errorMessage)
    {
        return new DbQueryResult(DbQueryResultStatus.Error, errorMessage);
    }

    public static DbQueryResult NotFound(string errorMessage)
    {
        return new DbQueryResult(DbQueryResultStatus.NotFound, errorMessage);
    }
}

public class DbQueryResult<T>
{
    public T Result { get; }

    public DbQueryResultStatus Status { get; }
    
    public string ErrorMessage { get; }
    
    private DbQueryResult(T value, DbQueryResultStatus status, string errorMessage = null)
    {
        this.Result = value;
        this.Status = status;
        this.ErrorMessage = errorMessage;
    }

    public void EnsureSuccess()
    {
        if (this.Status != DbQueryResultStatus.Ok)
        {
            throw new DatabaseQueryException(this.ErrorMessage);
        }
    }

    public T GetValueOrThrow()
    {
        this.EnsureSuccess();
        return Result;
    }
    
    public static DbQueryResult<T> Ok(T value)
    {
        return new DbQueryResult<T>(value, DbQueryResultStatus.Ok);
    }
    
    public static DbQueryResult<T> Error(string errorMessage)
    {
        return new DbQueryResult<T>(default, DbQueryResultStatus.Error, errorMessage);
    }

    public static DbQueryResult<T> NotFound(string errorMessage)
    {
        return new DbQueryResult<T>(default, DbQueryResultStatus.NotFound, errorMessage);
    }

    public static DbQueryResult<T> Conflict(string errorMessage)
    {
        return new DbQueryResult<T>(default, DbQueryResultStatus.Conflict, errorMessage);
    }
}