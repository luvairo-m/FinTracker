using MediatR;

namespace FinTracker.Logic.Handlers.Currency.RemoveCurrency;

public class RemoveCurrencyCommand : IRequest
{
    public Guid Id { get; set; }
}