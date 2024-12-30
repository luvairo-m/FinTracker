namespace FinTracker.Logic.Models.Bill;

public struct GetBillModel
{
    public Guid BillId { get; set; }

    public string Title { get; set; }

    public int Amount { get; set; }
}