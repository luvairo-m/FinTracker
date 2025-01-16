using AutoMapper;
using FinTracker.Dal.Models.Bills;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.GetAccounts;

internal class GetAccountsCommandHandler : IRequestHandler<GetAccountsCommand, ICollection<GetAccountModel>>
{
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public GetAccountsCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task<ICollection<GetAccountModel>> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
    {
        var getResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));
        getResult.EnsureSuccess();
        
        return mapper.Map<ICollection<Dal.Models.Accounts.Account>, ICollection<GetAccountModel>>(getResult.Result);
    }
}