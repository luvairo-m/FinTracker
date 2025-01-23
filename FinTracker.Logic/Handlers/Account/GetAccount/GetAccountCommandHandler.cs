using AutoMapper;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.GetAccount;

// ReSharper disable once UnusedType.Global
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
        var searchResult = await accountRepository.SearchAsync(mapper.Map<AccountSearch>(request));

        searchResult.EnsureSuccess();

        var account = searchResult.Result.FirstOrDefault();

        return mapper.Map<GetAccountModel>(account);
    }
}