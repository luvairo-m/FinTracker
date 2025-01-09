using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.UpdateBill;

internal class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand>
{
    private readonly IBillRepository _billRepository;
    private readonly IMapper _mapper;

    public UpdateBillCommandHandler(IBillRepository billRepository, IMapper mapper)
    {
        _billRepository = billRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateBillCommand request, CancellationToken cancellationToken)
    {
        var gettingBillsResult = await _billRepository.SearchAsync(new BillSearch { Id = request.Id });
        gettingBillsResult.EnsureSuccess();
        
        var existingBill = gettingBillsResult.Result.FirstOrDefault();
        
        var updatedBill = _mapper.Map(request, existingBill);
        
        var updateBillResult = await _billRepository.UpdateAsync(updatedBill);
        updateBillResult.EnsureSuccess();
    }
}