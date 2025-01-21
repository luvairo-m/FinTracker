using AutoMapper;
using FinTracker.Dal.Logic;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using MediatR;

namespace FinTracker.Logic.Handlers.Account.UpdateAccount;

internal class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
{
    private readonly IAccountRepository accountRepository;
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public UpdateAccountCommandHandler(
        IAccountRepository accountRepository, 
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        this.accountRepository = accountRepository;
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        await this.ValidateRequest(request);

        var updateResult = await accountRepository.UpdateAsync(mapper.Map<Dal.Models.Accounts.Account>(request));
        updateResult.EnsureSuccess();
    }

    private async Task ValidateRequest(UpdateAccountCommand request)
    {
        if (request.CurrencyId != null || request.Balance != null)
        {
            var searchPaymentResult = await this.paymentRepository.SearchAsync(new PaymentSearch { AccountId = request.Id });
            var accountIsEmpty = searchPaymentResult.Status == DbQueryResultStatus.NotFound;
            
            if (searchPaymentResult.Status != DbQueryResultStatus.NotFound)
            {
                searchPaymentResult.EnsureSuccess();
            }

            // Пока не умеем пересчитывать платежи в рамках счета на другую валюту.
            if (request.CurrencyId != null && !accountIsEmpty)
            {
                throw new ForbiddenOperation("Currency can only be updated for accounts without payments.");
            }

            if (request.Balance != null && !accountIsEmpty)
            {
                throw new ForbiddenOperation("Balance can only be updated for accounts without payments.");
            }
        }
    }
}