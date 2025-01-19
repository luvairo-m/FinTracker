using AutoMapper;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.CreateAccount;

internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountModel>
{
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task<CreateAccountModel> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = mapper.Map<Dal.Models.Accounts.Account>(request);
        
        var createAccountResult = await accountRepository.AddAsync(account);
        createAccountResult.EnsureSuccess();

        return new CreateAccountModel { Id = createAccountResult.Result };
    }
}