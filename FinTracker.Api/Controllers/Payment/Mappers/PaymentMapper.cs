using AutoMapper;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Handlers.Payment.UpdatePayment;
using FinTracker.Logic.Models.Payment;

namespace FinTracker.Api.Controllers.Payment.Mappers;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<CreatePaymentModel, CreatePaymentResponse>();
        
        CreateMap<GetPaymentModel, GetPaymentResponse>();
        
        CreateMap<GetPaymentsModel, GetPaymentsResponse>();
        
        CreateMap<Dal.Models.Payments.Payment, GetPaymentModel>();

        CreateMap<UpdatePaymentCommand, Dal.Models.Payments.Payment>()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore());
    }
}