using AutoMapper;
using FinTracker.Api.Controllers.Bill.Dto.Requests;
using FinTracker.Api.Controllers.Bill.Dto.Responses;
using FinTracker.Logic.Handlers.Bill.GetBills;
using FinTracker.Logic.Handlers.Bill.UpdateBill;
using FinTracker.Logic.Models.Bill;

namespace FinTracker.Api.Controllers.Bill.Mappers;

public class BillMapper : Profile
{
    public BillMapper()
    {
        CreateMap<CreateBillModel, CreateBillResponse>();
        
        CreateMap<GetBillModel, GetBillResponse>();
        
        CreateMap<GetBillsRequest, GetBillsCommand>();
        
        CreateMap<Dal.Models.Bills.Bill, GetBillModel>();
        
        CreateMap<UpdateBillCommand, Dal.Models.Bills.Bill>()
            .ForMember(dest => dest.Balance, opt => opt.Ignore());
    }
}