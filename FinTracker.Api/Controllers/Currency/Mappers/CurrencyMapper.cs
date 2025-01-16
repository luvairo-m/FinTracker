using System;
using AutoMapper;
using FinTracker.Api.Controllers.Currency.Dto.Requests;
using FinTracker.Api.Controllers.Currency.Dto.Responses;
using FinTracker.Logic.Handlers.Currency.CreateCurrency;
using FinTracker.Logic.Handlers.Currency.GetCurrencies;
using FinTracker.Logic.Handlers.Currency.GetCurrency;
using FinTracker.Logic.Handlers.Currency.RemoveCurrency;
using FinTracker.Logic.Handlers.Currency.UpdateCurrency;
using FinTracker.Logic.Models.Currency;

namespace FinTracker.Api.Controllers.Currency.Mappers;

public class CurrencyMapper : Profile
{
    public CurrencyMapper()
    {
        CreateMap<CreateCurrencyRequest, CreateCurrencyCommand>();
        
        CreateMap<CreateCurrencyModel, CreateCurrencyResponse>();

        CreateMap<Guid, GetCurrencyCommand>()
            .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src));

        CreateMap<GetCurrencyModel, GetCurrencyResponse>();

        CreateMap<GetCurrenciesRequest, GetCurrenciesCommand>();
        
        CreateMap<GetCurrenciesModel, GetCurrenciesResponse>();
        
        CreateMap<(Guid currencyId, UpdateCurrencyRequest updateCurrencyRequest), UpdateCurrencyCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.currencyId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.updateCurrencyRequest.Title))
            .ForMember(dest => dest.Sign, opt => opt.MapFrom(src => src.updateCurrencyRequest.Sign));
        
        CreateMap<Guid, RemoveCurrencyCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
    }
}