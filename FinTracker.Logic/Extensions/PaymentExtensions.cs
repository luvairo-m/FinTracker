using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Handlers.Payment.UpdatePayment;

namespace FinTracker.Logic.Extensions;

public static class PaymentExtensions
{
    public static Payment GetUpdated(this Payment payment, UpdatePaymentCommand updateCommand)
    {
        return new Payment
        {
            Id = payment.Id,
            Title = updateCommand.Title ?? payment.Title,
            Description = updateCommand.Description ?? payment.Description,
            Amount = updateCommand.Amount ?? payment.Amount,
            Type = updateCommand.Type ?? payment.Type,
            Date = updateCommand.Date ?? payment.Date,
            AccountId = updateCommand.AccountId ?? payment.AccountId,
            Categories = payment.Categories
        };
    }
}