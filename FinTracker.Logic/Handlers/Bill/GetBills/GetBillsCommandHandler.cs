using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Logic.Models.Bill;
using MediatR;

namespace FinTracker.Logic.Handlers.Bill.GetBills;

internal class GetBillsCommandHandler : IRequestHandler<GetBillsCommand, ICollection<GetBillModel>>
{
    private readonly IBillRepository billRepository;
    private readonly IMapper mapper;

    public GetBillsCommandHandler(IBillRepository billRepository, IMapper mapper)
    {
        this.billRepository = billRepository;
        this.mapper = mapper;
    }

    public async Task<ICollection<GetBillModel>> Handle(GetBillsCommand request, CancellationToken cancellationToken)
    {
        var getResult = await billRepository.SearchAsync(new BillSearch
        {
            Id = request.Id,
            TitleSubstring = request.Title,
            CurrencyId = request.CurrencyId
        });
        getResult.EnsureSuccess();
        
        return mapper.Map<ICollection<Dal.Models.Bills.Bill>, ICollection<GetBillModel>>(getResult.Result);
    }
}