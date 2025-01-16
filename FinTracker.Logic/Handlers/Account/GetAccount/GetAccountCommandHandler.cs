using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.GetAccount;

internal class GetAccountCommandHandler : IRequestHandler<GetAccountCommand, GetAccountModel>
{
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public GetAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }
    
    public async Task<GetAccountModel> Handle(GetAccountCommand request, CancellationToken cancellationToken)
    {
        var gettingBillsResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));

        gettingBillsResult.EnsureSuccess();

        var bill = gettingBillsResult.Result.FirstOrDefault();

        return mapper.Map<GetAccountModel>(bill);
    }
}