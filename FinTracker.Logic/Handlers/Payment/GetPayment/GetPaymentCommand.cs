using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayment;

public class GetPaymentCommand : IRequest<GetPaymentModel>
{
    public GetPaymentCommand(Guid paymentId)
    {
        PaymentId = paymentId;
    }

    public Guid PaymentId { get; set; }
}