using System;
using AutoMapper;
using FinTracker.Api.Configuration.Swagger;
using FinTracker.Dal;
using FinTracker.Logic.Handlers.Payment.CreatePayment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vostok.Logging.Abstractions;

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

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePaymentCommand).Assembly));
        
        services.AddSwaggerDocumentation();

        services.AddDal();
        
        services.AddSingleton<ILog>(new CompositeLog());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
        
        if (env.IsDevelopment())
        {
            app.UseSwaggerDocumentation();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}