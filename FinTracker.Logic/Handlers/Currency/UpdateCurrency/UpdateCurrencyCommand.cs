using MediatR;

namespace FinTracker.Logic.Handlers.Currency.UpdateCurrency;

public class UpdateCurrencyCommand : IRequest
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Sign { get; set; }
}