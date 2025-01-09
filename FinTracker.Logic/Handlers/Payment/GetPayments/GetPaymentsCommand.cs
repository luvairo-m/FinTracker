using FinTracker.Dal.Logic;
using FinTracker.Dal.Logic.Attributes;
using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayments;

public class GetPaymentsCommand : IRequest<GetPaymentsModel>
{
    public GetPaymentsCommand(decimal? minAmount, decimal? maxAmount, OperationType[] types, DateTime? minDate, 
        DateTime? maxDate, int[] months, int[] years, Guid? billId)
    {
        MinAmount = minAmount;
        MaxAmount = maxAmount;
        Types = types;
        MinDate = minDate;
        MaxDate = maxDate;
        Months = months;
        Years = years;
        BillId = billId;
    }
    
    public decimal? MinAmount { get; set; }
    
    public decimal? MaxAmount { get; set; }
    
    public OperationType[] Types { get; set; }

    public DateTime? MinDate { get; set; }

    public DateTime? MaxDate { get; set; }

    public int[] Months { get; set; }

    public int[] Years { get; set; }

    public Guid? BillId { get; set; }
}