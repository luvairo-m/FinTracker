using FinTracker.Logic.Managers.Bill;
using FinTracker.Logic.Managers.Bill.Interfaces;
using FinTracker.Logic.Managers.Category;
using FinTracker.Logic.Managers.Category.Interfaces;
using FinTracker.Logic.Managers.Payment;
using FinTracker.Logic.Managers.Payment.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Logic;

public static class ForStartup
{
    public static IServiceCollection AddLogicService(this IServiceCollection services)
    {
        services.AddScoped<IBillManager, BillManager>();
        services.AddScoped<ICategoryManager, CategoryManager>();
        services.AddScoped<IPaymentManager, PaymentManager>();
        
        return services;
    }
}