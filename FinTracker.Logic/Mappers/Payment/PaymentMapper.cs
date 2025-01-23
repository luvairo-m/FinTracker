using AutoMapper;
using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Handlers.Payment.CreatePayment;
using FinTracker.Logic.Handlers.Payment.GetPayment;
using FinTracker.Logic.Handlers.Payment.GetPayments;
using FinTracker.Logic.Models.Payment;

namespace FinTracker.Logic.Mappers.Payment;

// ReSharper disable once UnusedType.Global
public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<GetPaymentCommand, PaymentSearch>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId));

        CreateMap<GetPaymentsCommand, PaymentSearch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<CreatePaymentCommand, Dal.Models.Payments.Payment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<Dal.Models.Payments.Payment, GetPaymentModel>();
    }
}