using AutoMapper;
using FinTracker.Dal.Models.Currencies;
using FinTracker.Logic.Handlers.Currency.CreateCurrency;
using FinTracker.Logic.Handlers.Currency.GetCurrencies;
using FinTracker.Logic.Handlers.Currency.GetCurrency;
using FinTracker.Logic.Handlers.Currency.UpdateCurrency;
using FinTracker.Logic.Models.Currency;

namespace FinTracker.Logic.Mappers.Currency;

public class CurrencyMapper : Profile
{
    public CurrencyMapper()
    {
        CreateMap<CreateCurrencyCommand, Dal.Models.Currencies.Currency>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<GetCurrenciesCommand, CurrencySearch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TitleSubstring, opt => opt.MapFrom(src => src.TitleSubstring));

        CreateMap<GetCurrencyCommand, CurrencySearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CurrencyId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore());
        
        CreateMap<Dal.Models.Currencies.Currency, GetCurrencyModel>();
        
        CreateMap<UpdateCurrencyCommand, Dal.Models.Currencies.Currency>();
    }
}