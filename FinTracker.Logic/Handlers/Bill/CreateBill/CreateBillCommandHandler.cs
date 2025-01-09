using FinTracker.Dal.Repositories.Bills;
using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.CreateBill;

internal class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, CreateBillModel>
{
    private readonly IBillRepository _billRepository;

    public CreateBillCommandHandler(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<CreateBillModel> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var newBill = new Dal.Models.Bills.Bill
        {
            Balance = request.Balance,
            Title = request.Title,
            Description = request.Description,
            CurrencyId = request.CurrencyId
        };
        
        var creatingBillResult = await _billRepository.AddAsync(newBill);
        
        creatingBillResult.EnsureSuccess();

        return new CreateBillModel { Id = creatingBillResult.Result };
    }
}