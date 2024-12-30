using System.ComponentModel.DataAnnotations;
using FinTracker.Logic.Models.Payment;
using FinTracker.Logic.Models.Payment.Enums;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

public class CreatePaymentCommand : IRequest<CreatePaymentModel>
{
    public CreatePaymentCommand(string title, string description, Guid billId, decimal amount, FinancialOperation operation)
    {
        Title = title; 
        Description = description;
        BillId = billId;
        Amount = amount;
        Operation = operation;
    }
    
    public string Title { get; set; }

    public string Description { get; set; }

    public Guid BillId { get; set; }

    public decimal Amount { get; set; }

    public FinancialOperation Operation { get; set; }
}