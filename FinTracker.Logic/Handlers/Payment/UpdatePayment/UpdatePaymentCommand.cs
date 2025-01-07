using FinTracker.Dal.Models.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

public class UpdatePaymentCommand : IRequest
{
    public UpdatePaymentCommand(
        Guid paymentId, string title, string description, decimal amount, Guid billId, Guid currencyId, Guid categoryId, OperationType type)
    {
        PaymentId = paymentId;
        Title = title; 
        Description = description;
        Amount = amount;
        BillId = billId;
        CurrencyId = currencyId;
        CategoryId = categoryId;
        Type = type;
    }
    
    public Guid PaymentId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
    
    public decimal Amount { get; set; }

    public Guid BillId { get; set; }

    public Guid CurrencyId { get; set; }
    
    public Guid CategoryId { get; set; }

    public OperationType Type { get; set; }
}