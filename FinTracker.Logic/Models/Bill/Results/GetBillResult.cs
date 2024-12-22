namespace FinTracker.Logic.Models.Bill.Results;

public struct GetBillResult
{
    public Guid BillId { get; set; }

    public string Title { get; set; }

    public int Amount { get; set; }
}