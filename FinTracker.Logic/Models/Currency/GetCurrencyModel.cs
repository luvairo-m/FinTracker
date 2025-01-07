namespace FinTracker.Logic.Models.Currency;

public struct GetCurrencyModel
{
    public Guid CurrencyId { get; set; }

    public string Title { get; set; }

    public string Sign { get; set; }
}