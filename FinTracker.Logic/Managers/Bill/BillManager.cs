using FinTracker.Logic.Managers.Bill.Interfaces;
using FinTracker.Logic.Models.Bill.Params;
using FinTracker.Logic.Models.Bill.Results;

namespace FinTracker.Logic.Managers.Bill;

public class BillManager : IBillManager
{
    public Task<CreateBillResult> CreateBill(CreateBillParam createBillParam)
    {
        throw new NotImplementedException();
    }

    public Task<GetBillResult> GetBill(Guid billId)
    {
        throw new NotImplementedException();
    }

    public Task<GetBillsResult> GetBills()
    {
        throw new NotImplementedException();
    }

    public Task UpdateBill(UpdateBillParam updateBillParam)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBill(Guid billId)
    {
        throw new NotImplementedException();
    }
}