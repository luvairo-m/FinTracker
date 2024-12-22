namespace FinTracker.Logic.Models.Bill.Params;

public struct CreateBillParam
{
    public string Title { get; set; }

    public int Amount { get; set; }
}