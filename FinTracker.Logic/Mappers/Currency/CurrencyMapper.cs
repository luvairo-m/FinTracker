using AutoMapper;
using FinTracker.Logic.Handlers.Currency.UpdateCurrency;
using FinTracker.Logic.Models.Currency;

namespace FinTracker.Logic.Mappers.Currency;

public class CurrencyMapper : Profile
{
    public CurrencyMapper()
    {
        CreateMap<Dal.Models.Currencies.Currency, GetCurrencyModel>();
        
        CreateMap<UpdateCurrencyCommand, Dal.Models.Currencies.Currency>();
    }
}