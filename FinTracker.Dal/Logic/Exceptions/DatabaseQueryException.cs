using FinTracker.Infra.Exceptions;

namespace FinTracker.Dal.Logic.Exceptions;

public class DatabaseQueryException : CustomException
{
    public DatabaseQueryException(string message)
        : base(message)
    {
    }
}