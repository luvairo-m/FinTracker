using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Repositories.Accounts;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Dal.Repositories.Payments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Dal;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>(
            provider => new SqlConnectionFactory(provider.GetRequiredService<IConfiguration>().GetConnectionString("FinTracker")));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        
        return services;
    }
}