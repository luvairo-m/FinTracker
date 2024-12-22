using FinTracker.Logic.Models.Bill.Params;
using FinTracker.Logic.Models.Bill.Results;

namespace FinTracker.Logic.Managers.Bill.Interfaces;

public interface IBillManager
{
    Task<CreateBillResult> CreateBill(CreateBillParam createBillParam);

    Task<GetBillResult> GetBill(Guid billId);

    Task<GetBillsResult> GetBills();

    Task UpdateBill(UpdateBillParam updateBillParam);
    
    Task DeleteBill(Guid billId);
}