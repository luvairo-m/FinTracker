namespace FinTracker.Logic.Models.Bill.Params;

public struct UpdateBillParam
{
    public Guid BillId { get; set; }

    public string Title { get; set; }

    public int Amount { get; set; }
}