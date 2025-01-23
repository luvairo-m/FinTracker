using MediatR;

namespace FinTracker.Logic.Handlers.Payment.RemovePayment;

public class RemovePaymentCommand : IRequest
{
    public Guid Id { get; set; }
}