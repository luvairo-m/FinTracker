using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBill;

internal class GetBillCommandHandler : IRequestHandler<GetBillCommand, GetBillModel>
{
    private readonly IBillRepository _billRepository;
    private readonly IMapper _mapper;

    public GetBillCommandHandler(IBillRepository billRepository, IMapper mapper)
    {
        _billRepository = billRepository;
        _mapper = mapper;
    }

    public async Task<GetBillModel> Handle(GetBillCommand request, CancellationToken cancellationToken)
    {
        var gettingBillsResult = await _billRepository.SearchAsync(new BillSearch { Id = request.BillId });
        
        gettingBillsResult.EnsureSuccess();
        
        var bill = gettingBillsResult.Result.FirstOrDefault();
        
        return _mapper.Map<GetBillModel>(bill);
    }
}