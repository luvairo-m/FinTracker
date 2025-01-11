using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBill;

internal class GetBillCommandHandler : IRequestHandler<GetBillCommand, GetBillModel>
{
    private readonly IBillRepository billRepository;
    private readonly IMapper mapper;

    public GetBillCommandHandler(IBillRepository billRepository, IMapper mapper)
    {
        this.billRepository = billRepository;
        this.mapper = mapper;
    }

    public async Task<GetBillModel> Handle(GetBillCommand request, CancellationToken cancellationToken)
    {
        var gettingBillsResult = await billRepository.SearchAsync(new BillSearch { Id = request.BillId });
        gettingBillsResult.EnsureSuccess();
        
        var bill = gettingBillsResult.Result.FirstOrDefault();
        
        return mapper.Map<GetBillModel>(bill);
    }
}