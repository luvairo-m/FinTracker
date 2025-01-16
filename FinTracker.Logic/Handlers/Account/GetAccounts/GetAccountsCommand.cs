using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.GetAccounts;

public class GetAccountsCommand : IRequest<ICollection<GetAccountModel>>
{
    public Guid? Id { get; set; }

    public string TitleSubstring { get; set; }
    
    public Guid? CurrencyId { get; set; }
}