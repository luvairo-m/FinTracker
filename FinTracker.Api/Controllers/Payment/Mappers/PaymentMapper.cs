using System;
using AutoMapper;
using FinTracker.Api.Controllers.Payment.Dto.Requests;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Handlers.Payment.CreatePayment;
using FinTracker.Logic.Handlers.Payment.GetPayment;
using FinTracker.Logic.Handlers.Payment.GetPayments;
using FinTracker.Logic.Handlers.Payment.RemovePayment;
using FinTracker.Logic.Handlers.Payment.UpdatePayment;
using FinTracker.Logic.Models.Payment;

namespace FinTracker.Api.Controllers.Payment.Mappers;

// ReSharper disable once UnusedType.Global
public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<CreatePaymentRequest, CreatePaymentCommand>();
        
        CreateMap<CreatePaymentModel, CreatePaymentResponse>();

        CreateMap<Guid, GetPaymentCommand>()
            .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src));
        
        CreateMap<GetPaymentModel, GetPaymentResponse>();
        
        CreateMap<GetPaymentsRequest, GetPaymentsCommand>();
        
        CreateMap<(Guid paymentId, UpdatePaymentRequest updatePaymentRequest), UpdatePaymentCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.paymentId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.updatePaymentRequest.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.updatePaymentRequest.Description))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.updatePaymentRequest.Amount))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.updatePaymentRequest.AccountId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.updatePaymentRequest.Type))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.updatePaymentRequest.Date));
        
        CreateMap<Guid, RemovePaymentCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
    }
}