using AutoMapper;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Models.Payment;

namespace FinTracker.Api.Controllers.Payment.Mappers;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<CreatePaymentModel, CreatePaymentResponse>();
        
        CreateMap<GetPaymentModel, GetPaymentResponse>();
        
        CreateMap<GetPaymentsModel, GetPaymentsResponse>();
    }
}