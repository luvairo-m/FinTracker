using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;

namespace FinTracker.Logic.Extensions;

public static class AccountExtensions
{
    public static void ApplyPayment(this Account account, Payment payment)
    {
        RecalculateAccountBalance(account, payment, apply: true);
    }

    public static void RevertPayment(this Account account, Payment payment)
    {
        RecalculateAccountBalance(account, payment, apply: false);
    }

    private static void RecalculateAccountBalance(this Account account, Payment payment, bool apply)
    {
        var (amount, operationType) = (payment!.Amount!.Value, payment!.Type!.Value);
        var accountBalance = account!.Balance!.Value;

        var balance = apply
            ? operationType == OperationType.Income ? accountBalance + amount : accountBalance - amount
            : operationType == OperationType.Income ? accountBalance - amount : accountBalance + amount;

        if (balance < 0)
        {
            throw new ForbiddenOperation("The operation will result in a negative account balance.");
        }

        account.Balance = balance;
    }
}