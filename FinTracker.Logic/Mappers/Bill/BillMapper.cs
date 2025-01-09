using AutoMapper;
using FinTracker.Logic.Handlers.Bill.UpdateBill;
using FinTracker.Logic.Models.Bill;

namespace FinTracker.Logic.Mappers.Bill;

public class BillMapper : Profile
{
    public BillMapper()
    {
        CreateMap<Dal.Models.Bills.Bill, GetBillModel>();

        CreateMap<UpdateBillCommand, Dal.Models.Bills.Bill>()
            .ForMember(dest => dest.Balance, opt => opt.Condition(src => src.Balance != null))
            .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.CurrencyId, opt => opt.Condition(src => src.CurrencyId != null));
    }
}