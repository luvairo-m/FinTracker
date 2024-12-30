using FinTracker.Logic.Models.Payment.Enums;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

public class UpdatePaymentCommand : IRequest
{
    public UpdatePaymentCommand(
        Guid paymentId, string title, string description, Guid billId, decimal amount, FinancialOperation operation)
    {
        PaymentId = paymentId;
        Title = title;
        Description = description;
        BillId = billId;
        Amount = amount;
        Operation = operation;
    }
    
    public Guid PaymentId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid BillId { get; set; }

    public decimal Amount { get; set; }

    public FinancialOperation Operation { get; set; }
}