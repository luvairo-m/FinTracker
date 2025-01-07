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
        CreateMap<CreateBillModel, CreateBillResponse>();

        CreateMap<GetBillModel, GetBillResponse>();
        
        CreateMap<GetBillsModel, GetBillsResponse>();
    }
}