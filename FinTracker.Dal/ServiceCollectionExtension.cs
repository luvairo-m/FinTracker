using FinTracker.Dal.Logic.Connections;
using FinTracker.Dal.Repositories.Bills;
using FinTracker.Dal.Repositories.Categories;
using FinTracker.Dal.Repositories.Currencies;
using FinTracker.Dal.Repositories.Payments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Logging.Abstractions;

namespace FinTracker.Dal;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddTransient<ISqlConnectionFactory, SqlConnectionFactory>(
            provider => new SqlConnectionFactory(provider.GetRequiredService<IConfiguration>().GetConnectionString("FinTracker")));

        services.AddTransient<CategoryRepository>();
        services.AddTransient<BillRepository>();
        services.AddTransient<CurrencyRepository>();
        services.AddTransient<PaymentRepository>();
        
        return services;
    }
}