using FinTracker.Api.Configuration.Mapper;
using FinTracker.Api.Configuration.Swagger;
using FinTracker.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        services.AddMapper();

        services.AddLogicService();
        
        services.AddSwaggerDocumentation();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwaggerDocumentation();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}