namespace FinTracker.Dal.Logic.Exceptions;

public class DatabaseQueryException : Exception
{
    public DatabaseQueryException(string message)
        : base(message)
    {
    }
}