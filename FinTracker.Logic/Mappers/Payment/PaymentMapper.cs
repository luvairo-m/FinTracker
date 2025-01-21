using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Models.Payments;
using FinTracker.Logic.Handlers.Payment.CreatePayment;
using FinTracker.Logic.Handlers.Payment.GetPayment;
using FinTracker.Logic.Handlers.Payment.GetPayments;
using FinTracker.Logic.Handlers.Payment.RemovePayment;
using FinTracker.Logic.Handlers.Payment.UpdatePayment;
using FinTracker.Logic.Models.Payment;

namespace FinTracker.Logic.Mappers.Payment;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<CreatePaymentCommand, Dal.Models.Payments.Payment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<GetPaymentsCommand, PaymentSearch>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TitleSubstring, opt => opt.MapFrom(src => src.TitleSubstring))
            .ForMember(dest => dest.MinAmount, opt => opt.MapFrom(src => src.MinAmount))
            .ForMember(dest => dest.MaxAmount, opt => opt.MapFrom(src => src.MaxAmount))
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types))
            .ForMember(dest => dest.MaxDate, opt => opt.MapFrom(src => src.MaxDate))
            .ForMember(dest => dest.Months, opt => opt.MapFrom(src => src.Months))
            .ForMember(dest => dest.Years, opt => opt.MapFrom(src => src.Years))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.Categories, opt => opt.Ignore());
        
        CreateMap<Dal.Models.Payments.Payment, GetPaymentModel>();

        CreateMap<GetPaymentCommand, PaymentSearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.MinAmount, opt => opt.Ignore())
            .ForMember(dest => dest.MaxAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Types, opt => opt.Ignore())
            .ForMember(dest => dest.MinDate, opt => opt.Ignore())
            .ForMember(dest => dest.MaxDate, opt => opt.Ignore())
            .ForMember(dest => dest.Months, opt => opt.Ignore())
            .ForMember(dest => dest.Years, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore());

        CreateMap<UpdatePaymentCommand, PaymentSearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.MinAmount, opt => opt.Ignore())
            .ForMember(dest => dest.MaxAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Types, opt => opt.Ignore())
            .ForMember(dest => dest.MinDate, opt => opt.Ignore())
            .ForMember(dest => dest.MaxDate, opt => opt.Ignore())
            .ForMember(dest => dest.Months, opt => opt.Ignore())
            .ForMember(dest => dest.Years, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore());

        CreateMap<Dal.Models.Payments.Payment, AccountSearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.CurrencyId, opt => opt.Ignore());
        
        CreateMap<UpdatePaymentCommand, AccountSearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.CurrencyId, opt => opt.Ignore());
        
        CreateMap<UpdatePaymentCommand, Dal.Models.Payments.Payment>()
            .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.Amount, opt => opt.Condition(src => src.Amount != null))
            .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != null))
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Condition(src => src.AccountId != null))
            .ForMember(dest => dest.Categories, opt => opt.Ignore());

        CreateMap<RemovePaymentCommand, PaymentSearch>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TitleSubstring, opt => opt.Ignore())
            .ForMember(dest => dest.MinAmount, opt => opt.Ignore())
            .ForMember(dest => dest.MaxAmount, opt => opt.Ignore())
            .ForMember(dest => dest.Types, opt => opt.Ignore())
            .ForMember(dest => dest.MinDate, opt => opt.Ignore())
            .ForMember(dest => dest.MaxDate, opt => opt.Ignore())
            .ForMember(dest => dest.Months, opt => opt.Ignore())
            .ForMember(dest => dest.Years, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore());
    }
}