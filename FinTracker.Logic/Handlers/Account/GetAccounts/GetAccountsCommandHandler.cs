using AutoMapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.GetAccounts;

// ReSharper disable once UnusedType.Global
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

        if (getResult.Status == DbQueryResultStatus.NotFound)
        {
            return Array.Empty<GetAccountModel>();
        }
        
        getResult.EnsureSuccess();
        
        return mapper.Map<ICollection<Dal.Models.Accounts.Account>, ICollection<GetAccountModel>>(getResult.Result);
    }
}