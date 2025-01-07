using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

public class CreatePaymentCommand : IRequest<CreatePaymentModel>
{
    public CreatePaymentCommand(
        string title, string description, decimal amount, Guid billId, Guid currencyId, Guid categoryId, OperationType type)
    {
        Title = title; 
        Description = description;
        Amount = amount;
        BillId = billId;
        CurrencyId = currencyId;
        CategoryId = categoryId;
        Type = type;
    }
    
    public string Title { get; set; }

    public string Description { get; set; }
    
    public decimal Amount { get; set; }

    public Guid BillId { get; set; }

    public Guid CurrencyId { get; set; }
    
    public Guid CategoryId { get; set; }

    public OperationType Type { get; set; }
}