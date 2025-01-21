using AutoMapper;
using FinTracker.Api.Configuration.Middleware;
using FinTracker.Api.Configuration.Swagger;
using FinTracker.Api.Controllers.Category.Mappers;
using FinTracker.Dal.Migrations;
using FinTracker.Dal;
using FinTracker.Logic.Handlers.Payment.CreatePayment;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

namespace FinTracker.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddApiVersioning();

        services.AddLogging(builder => builder.AddFluentMigratorConsole());
        
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(
                configure => configure
                    .AddSqlServer()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("FinTracker"))
                    .ScanIn(typeof(Initial_202501081823).Assembly).For.Migrations());
        
        services.AddAutoMapper(
            cfg => cfg.AllowNullCollections = true,
            typeof(CategoryMapper),
            typeof(Logic.Mappers.Category.CategoryMapper));
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommand).Assembly));
        
        services.AddSwaggerDocumentation();

        services.AddDal();
        
        services.AddSingleton<ILog>(new ConsoleLog());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
        
        if (env.IsDevelopment())
        {
            app.UseSwaggerDocumentation();
        }

        app.UseRouting();

        app.UseMiddleware<ErrorHandlingMiddleware>();
        
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}