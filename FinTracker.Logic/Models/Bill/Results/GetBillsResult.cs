namespace FinTracker.Logic.Models.Bill.Results;

public struct GetBillsResult
{
    public ICollection<GetBillResult> Bills { get; set; }
}