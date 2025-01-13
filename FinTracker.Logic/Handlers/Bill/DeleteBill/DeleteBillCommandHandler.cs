using FinTracker.Dal.Repositories.Bills;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.DeleteBill;

internal class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand>
{
    private readonly IBillRepository billRepository;

    public DeleteBillCommandHandler(IBillRepository billRepository)
    {
        this.billRepository = billRepository;
    }

    public async Task Handle(DeleteBillCommand request, CancellationToken cancellationToken)
    {
        var deletionBillResult = await billRepository.RemoveAsync(request.BillId);
        deletionBillResult.EnsureSuccess();
    }
}