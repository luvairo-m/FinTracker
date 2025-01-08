using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

public class CreatePaymentCommand : IRequest<CreatePaymentModel>
{
    public CreatePaymentCommand(
        string title, string description, decimal? amount, Guid? billId, Guid? categoryId, OperationType? type)
    {
        Title = title; 
        Description = description;
        Amount = (decimal)amount!;
        BillId = (Guid)billId!;
        CategoryId = (Guid)categoryId!;
        Type = (OperationType)type!;
    }
    
    public string Title { get; set; }

    public string Description { get; set; }
    
    public decimal Amount { get; set; }

    public Guid BillId { get; set; }
    
    public Guid CategoryId { get; set; }

    public OperationType Type { get; set; }
}