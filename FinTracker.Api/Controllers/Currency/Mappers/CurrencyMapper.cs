using AutoMapper;
using FinTracker.Api.Controllers.Currency.Dto.Responses;
using FinTracker.Logic.Models.Currency;

namespace FinTracker.Api.Controllers.Currency.Mappers;

public class CurrencyMapper : Profile
{
    public CurrencyMapper()
    {
        CreateMap<CreateCurrencyResponse, CreateCurrencyModel>();

        CreateMap<GetCurrencyResponse, GetCurrencyModel>();

        CreateMap<GetCurrenciesResponse, GetCurrenciesModel>();
    }
}