using FinTracker.Dal.Models.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

public class UpdatePaymentCommand : IRequest
{
    public UpdatePaymentCommand(
        Guid id, string title, string description, decimal? amount, Guid? billId, OperationType? type)
    {
        Id = id;
        Title = title; 
        Description = description;
        Amount = amount;
        BillId = billId;
        Type = type;
    }
    
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
    
    public decimal? Amount { get; set; }

    public Guid? BillId { get; set; }

    public OperationType? Type { get; set; }
}