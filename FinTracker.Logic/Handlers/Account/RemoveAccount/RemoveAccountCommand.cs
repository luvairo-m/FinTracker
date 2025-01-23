using MediatR;

namespace FinTracker.Logic.Handlers.Account.RemoveAccount;

public class RemoveAccountCommand : IRequest
{
    public Guid AccountId { get; set; }
}