using System;
using AutoMapper;
using FinTracker.Api.Controllers.Bill.Dto.Requests;
using FinTracker.Api.Controllers.Bill.Dto.Responses;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Models.Bill.Params;
using FinTracker.Logic.Models.Bill.Results;
using FinTracker.Logic.Models.Payment.Results;
using Microsoft.AspNetCore.SignalR;

namespace FinTracker.Api.Controllers.Bill.Mappers;

public class BillMapper : Profile
{
    public BillMapper()
    {
        CreateMap<CreateBillRequest, CreateBillParam>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));

        CreateMap<CreateBillResult, CreateBillResponse>()
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.BillId));
        
        CreateMap<GetBillResult, GetBillResponse>()
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.BillId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));
        
        CreateMap<GetPaymentsResult, GetPaymentsResponse>()
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));

        CreateMap<(Guid, UpdateBillRequest), UpdateBillParam>()
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.Item1))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item2.Title))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Item2.Amount));
    }
}