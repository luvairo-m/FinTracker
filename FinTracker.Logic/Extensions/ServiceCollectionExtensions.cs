using FinTracker.Logic.Handlers.Payment.UpdatePayment.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Logic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPaymentUpdateStrategy, NonFinancialPaymentUpdateStrategy>();
        serviceCollection.AddScoped<IPaymentUpdateStrategy, SameAccountPaymentUpdateStrategy>();
        serviceCollection.AddScoped<IPaymentUpdateStrategy, NewAccountPaymentUpdateStrategy>();

        return serviceCollection;
    }
}