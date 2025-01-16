using FinTracker.Dal.Models.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

public class UpdatePaymentCommand : IRequest
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public decimal? Amount { get; set; }

    public Guid? AccountId { get; set; }

    public OperationType? Type { get; set; }
}