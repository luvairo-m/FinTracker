using AutoMapper;
using FinTracker.Logic.Handlers.Payment.UpdatePayment;
using FinTracker.Logic.Models.Payment;

namespace FinTracker.Logic.Mappers.Payment;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<Dal.Models.Payments.Payment, GetPaymentModel>();
        
        CreateMap<UpdatePaymentCommand, Dal.Models.Payments.Payment>()
            .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.Amount, opt => opt.Condition(src => src.Amount != null))
            .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != null))
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.BillId, opt => opt.Condition(src => src.BillId != null))
            .ForMember(dest => dest.Categories, opt => opt.Ignore());
    }
}