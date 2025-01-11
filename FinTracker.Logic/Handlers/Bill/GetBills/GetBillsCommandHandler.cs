using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBills;

internal class GetBillsCommandHandler : IRequestHandler<GetBillsCommand, GetBillsModel>
{
    private readonly IBillRepository billRepository;
    private readonly IMapper mapper;

    public GetBillsCommandHandler(IBillRepository billRepository, IMapper mapper)
    {
        this.billRepository = billRepository;
        this.mapper = mapper;
    }

    public async Task<GetBillsModel> Handle(GetBillsCommand request, CancellationToken cancellationToken)
    {
        var gettingBillsResult = await billRepository.SearchAsync(new BillSearch());
        gettingBillsResult.EnsureSuccess();

        return new GetBillsModel
        {
            Bills = mapper.Map<ICollection<GetBillModel>>(gettingBillsResult.Result)
        };
    }
}