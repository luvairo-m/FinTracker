using AutoMapper;
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
        if (request.CurrencyId != null)
        {
            var searchPaymentResult = await this.paymentRepository.SearchAsync(mapper.Map<PaymentSearch>(request));
            searchPaymentResult.EnsureSuccess();

            // Пока не умеем пересчитывать платежи в рамках счета на другую валюту.
            if (searchPaymentResult.Result.Count > 0)
            {
                throw new Exception("Currency updating can be applied only to empty accounts (0 payments).");
            }
        }
        
        (await accountRepository.UpdateAsync(mapper.Map<Dal.Models.Accounts.Account>(request))).EnsureSuccess();
    }
}