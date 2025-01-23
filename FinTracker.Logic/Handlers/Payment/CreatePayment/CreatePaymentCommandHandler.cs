using System.Transactions;
using AutoMapper;
using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models.Accounts;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Extensions;
using FinTracker.Logic.Models.Payment;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.CreatePayment;

// ReSharper disable once UnusedType.Global
internal class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentModel>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IAccountRepository accountRepository;
    private readonly IMapper mapper;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.accountRepository = accountRepository;
        this.mapper = mapper;
    }

    public async Task<CreatePaymentModel> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        using var transactionScope = TransactionUtils.CreateTransactionScope(IsolationLevel.RepeatableRead);

        var payment = mapper.Map<Dal.Models.Payments.Payment>(request);

        if (request.AccountId != null)
        {
            var account = (await this.accountRepository.SearchAsync(AccountSearch.ById(request.AccountId.Value))).FirstOrDefault();
            account.ApplyPayment(payment);
        
            (await this.accountRepository.UpdateAsync(account)).EnsureSuccess();
        }
        
        var paymentId = (await paymentRepository.AddAsync(payment)).GetValueOrThrow();
        
        transactionScope.Complete();

        return new CreatePaymentModel { PaymentId = paymentId };
    }
}