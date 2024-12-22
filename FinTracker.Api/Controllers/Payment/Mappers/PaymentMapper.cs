using System;
using AutoMapper;
using FinTracker.Api.Controllers.Payment.Dto.Requests;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Models.Payment.Params;
using FinTracker.Logic.Models.Payment.Results;

namespace FinTracker.Api.Controllers.Payment.Mappers;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<CreatePaymentRequest, CreatePaymentParam>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.BillId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Operation, opt => opt.MapFrom(src => src.Operation));
        
        CreateMap<GetPaymentResult, GetPaymentResponse>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.BillId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Operation, opt => opt.MapFrom(src => src.Operation))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate));
        
        CreateMap<GetPaymentsResponse, GetPaymentsResult>()
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));

        CreateMap<(Guid, UpdatePaymentRequest), UpdatePaymentParam>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Item1))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item2.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item2.Description))
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.Item2.BillId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Item2.Amount))
            .ForMember(dest => dest.Operation, opt => opt.MapFrom(src => src.Item2.Operation));
    }
}