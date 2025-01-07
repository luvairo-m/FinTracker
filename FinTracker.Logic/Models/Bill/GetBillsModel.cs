namespace FinTracker.Logic.Models.Bill;

public struct GetBillsModel
{
    public ICollection<GetBillModel> Bills { get; set; }
}