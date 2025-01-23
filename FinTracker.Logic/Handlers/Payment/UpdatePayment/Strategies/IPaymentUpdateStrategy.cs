namespace FinTracker.Logic.Handlers.Payment.UpdatePayment.Strategies;

public interface IPaymentUpdateStrategy
{
    bool Accept(UpdatePaymentCommand updateCommand);

    Task UpdateAsync(Dal.Models.Payments.Payment payment, UpdatePaymentCommand updateCommand);
}