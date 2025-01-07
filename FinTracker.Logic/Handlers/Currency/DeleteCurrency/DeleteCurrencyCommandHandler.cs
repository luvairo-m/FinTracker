using MediatR;

namespace FinTracker.Logic.Handlers.Currency.DeleteCurrency;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
{
    public Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}