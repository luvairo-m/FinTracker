using AutoMapper;
using FinTracker.Logic.Handlers.Bill.UpdateBill;
using FinTracker.Logic.Models.Bill;

namespace FinTracker.Logic.Mappers.Bill;

public class BillMapper : Profile
{
    public BillMapper()
    {
        CreateMap<Dal.Models.Bills.Bill, GetBillModel>();

        CreateMap<UpdateBillCommand, Dal.Models.Bills.Bill>();
    }
}