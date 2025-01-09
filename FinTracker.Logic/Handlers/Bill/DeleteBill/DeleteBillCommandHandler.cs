using FinTracker.Dal.Repositories.Bills;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.DeleteBill;

internal class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand>
{
    private readonly IBillRepository _billRepository;

    public DeleteBillCommandHandler(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task Handle(DeleteBillCommand request, CancellationToken cancellationToken)
    {
        var deletionBillResult = await _billRepository.RemoveAsync(request.BillId);
        
        deletionBillResult.EnsureSuccess();
    }
}