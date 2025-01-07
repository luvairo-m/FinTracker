namespace FinTracker.Logic.Models.Currency;

public struct GetCurrenciesModel
{
    public ICollection<GetCurrencyModel> Currencies { get; set; }
}