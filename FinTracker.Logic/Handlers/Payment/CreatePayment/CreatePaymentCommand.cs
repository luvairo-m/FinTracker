using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

public class CreatePaymentCommand : IRequest<CreatePaymentModel>
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    public decimal Amount { get; set; }

    public Guid AccountId { get; set; }
    
    public Guid CategoryId { get; set; }

    public OperationType Type { get; set; }
}