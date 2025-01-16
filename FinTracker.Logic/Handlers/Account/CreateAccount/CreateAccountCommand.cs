using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.CreateAccount;

public class CreateAccountCommand : IRequest<CreateAccountModel>
{
    public string Title { get; set; }

    public decimal Balance { get; set; }

    public string Description { get; set; }

    public Guid CurrencyId { get; set; }
}