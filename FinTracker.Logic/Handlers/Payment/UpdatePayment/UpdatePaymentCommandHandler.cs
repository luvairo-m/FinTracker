using System.Transactions;
using FinTracker.Dal.Logic.Extensions;
using FinTracker.Dal.Models.Payments;
using FinTracker.Dal.Repositories.Payments;
using FinTracker.Infra.Utils;
using FinTracker.Logic.Handlers.Payment.UpdatePayment.Strategies;
using MediatR;

namespace FinTracker.Logic.Handlers.Payment.UpdatePayment;

// ReSharper disable once UnusedType.Global
internal class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
{
    private readonly IEnumerable<IPaymentUpdateStrategy> strategies;
    private readonly IPaymentRepository paymentRepository;

    public UpdatePaymentCommandHandler(IEnumerable<IPaymentUpdateStrategy> strategies, IPaymentRepository paymentRepository)
    {
        this.strategies = strategies;
        this.paymentRepository = paymentRepository;
    }

    public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        using var transactionScope = TransactionUtils.CreateTransactionScope(IsolationLevel.RepeatableRead);

        var payment = (await this.paymentRepository.SearchAsync(PaymentSearch.ById(request.Id))).FirstOrDefault();

        await this.strategies.First(str => str.Accept(request)).UpdateAsync(payment, request);
        
        transactionScope.Complete();
    }
}