namespace FinTracker.Logic.Models.Account;

public struct GetAccountModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public decimal Balance { get; set; }

    public string Description { get; set; }

    public Guid CurrencyId { get; set; }
}