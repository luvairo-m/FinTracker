using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBills;

internal class GetBillsCommandHandler : IRequestHandler<GetBillsCommand, GetBillsModel>
{
    private readonly IBillRepository _billRepository;
    private readonly IMapper _mapper;

    public GetBillsCommandHandler(IBillRepository billRepository, IMapper mapper)
    {
        _billRepository = billRepository;
        _mapper = mapper;
    }

    public async Task<GetBillsModel> Handle(GetBillsCommand request, CancellationToken cancellationToken)
    {
        var gettingBillsResult = await _billRepository.SearchAsync(new BillSearch());
        gettingBillsResult.EnsureSuccess();

        return new GetBillsModel
        {
            Bills = _mapper.Map<ICollection<GetBillModel>>(gettingBillsResult.Result)
        };
    }
}