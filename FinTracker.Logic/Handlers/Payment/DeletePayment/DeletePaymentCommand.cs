using MediatR;

namespace FinTracker.Logic.Handlers.Payment.DeletePayment;

public class DeletePaymentCommand : IRequest
{
    public DeletePaymentCommand(Guid paymentId)
    {
        PaymentId = paymentId;
    }
    
    public Guid PaymentId { get; set; }
}