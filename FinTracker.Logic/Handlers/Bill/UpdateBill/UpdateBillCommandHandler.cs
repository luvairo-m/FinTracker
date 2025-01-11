using AutoMapper;
using FinTracker.Dal.Repositories.Bills;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.UpdateBill;

internal class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand>
{
    private readonly IBillRepository billRepository;
    private readonly IMapper mapper;

    public UpdateBillCommandHandler(IBillRepository billRepository, IMapper mapper)
    {
        this.billRepository = billRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateBillCommand request, CancellationToken cancellationToken)
    {
        var updatedBill = mapper.Map<Dal.Models.Bills.Bill>(request);
        
        var updateBillResult = await billRepository.UpdateAsync(updatedBill);
        updateBillResult.EnsureSuccess();
    }
}