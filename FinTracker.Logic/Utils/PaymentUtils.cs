using FinTracker.Dal.Models.Payments;

namespace FinTracker.Logic.Utils;

public static class PaymentUtils
{
    private const string NegativeBalanceMessage = "The operation will result in a negative account balance.";
    
    public static void EnsureApplyPayment(decimal accountBalance, decimal paymentAmount, OperationType paymentType)
    {
        EnsurePaymentInternal(accountBalance, paymentAmount, paymentType, OperationType.Outcome);
    }
    
    public static void EnsureRevertPayment(decimal accountBalance, decimal paymentAmount, OperationType paymentType)
    {
        EnsurePaymentInternal(accountBalance, paymentAmount, paymentType, OperationType.Income);
    }

    private static void EnsurePaymentInternal(
        decimal accountBalance, 
        decimal paymentAmount, 
        OperationType paymentType,
        OperationType dangerousOperation)
    {
        if (paymentType == dangerousOperation && accountBalance - paymentAmount < 0)
        {
            throw new ForbiddenOperation(NegativeBalanceMessage);
        }
    }
}