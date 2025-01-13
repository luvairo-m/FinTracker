using AutoMapper;
using FinTracker.Api.Controllers.Currency.Dto.Responses;
using FinTracker.Logic.Handlers.Currency.UpdateCurrency;
using FinTracker.Logic.Models.Currency;

namespace FinTracker.Api.Controllers.Currency.Mappers;

public class CurrencyMapper : Profile
{
    public CurrencyMapper()
    {
        CreateMap<CreateCurrencyModel, CreateCurrencyResponse>();

        CreateMap<GetCurrencyModel, GetCurrencyResponse>();

        CreateMap<GetCurrenciesModel, GetCurrenciesResponse>();
        
        CreateMap<Dal.Models.Currencies.Currency, GetCurrencyModel>();

        CreateMap<UpdateCurrencyCommand, Dal.Models.Currencies.Currency>();
    }
}