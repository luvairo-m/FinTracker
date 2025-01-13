using FinTracker.Infra.Exceptions;

namespace FinTracker.Logic;

public class ForbiddenOperation : CustomException
{
    public ForbiddenOperation()
    {
    }

    public ForbiddenOperation(string message) : base(message)
    {
    }
}