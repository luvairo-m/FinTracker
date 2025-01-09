namespace FinTracker.Infra.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException()
    {
    }

    protected CustomException(string message) : base(message)
    {
    }
}