using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.GetPayments;

public class GetPaymentsCommand : IRequest<GetPaymentsModel>
{
    public string TitleSubstring { get; set; }

    public decimal? MinAmount { get; set; }
    
    public decimal? MaxAmount { get; set; }
    
    public OperationType[] Types { get; set; }

    public DateTime? MinDate { get; set; }

    public DateTime? MaxDate { get; set; }

    public int[] Months { get; set; }

    public int[] Years { get; set; }

    public Guid? AccountId { get; set; }
}