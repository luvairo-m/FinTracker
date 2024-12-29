using AutoMapper;
using FinTracker.Api.Controllers.Bill.Dto.Responses;
using FinTracker.Api.Controllers.Payment.Dto.Responses;
using FinTracker.Logic.Models.Bill;
using FinTracker.Logic.Models.Payment;

namespace FinTracker.Api.Controllers.Bill.Mappers;

public class BillMapper : Profile
{
    public BillMapper()
    {
        CreateMap<CreateBillModel, CreateBillResponse>()
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.BillId));
        
        CreateMap<GetBillModel, GetBillResponse>()
            .ForMember(dest => dest.BillId, opt => opt.MapFrom(src => src.BillId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));
        
        CreateMap<GetPaymentsModel, GetPaymentsResponse>()
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));
    }
}