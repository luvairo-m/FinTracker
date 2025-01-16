using FinTracker.Logic.Models.Account;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.GetAccount;

public class GetAccountCommand : IRequest<GetAccountModel>
{
    public Guid BillId { get; set; }
}