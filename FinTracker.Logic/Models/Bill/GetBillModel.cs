namespace FinTracker.Logic.Models.Bill;

public struct GetBillModel
{
    public Guid BillId { get; set; }

    public string Title { get; set; }

    public decimal Balance { get; set; }

    public string Description { get; set; }

    public Guid CurrencyId { get; set; }
}