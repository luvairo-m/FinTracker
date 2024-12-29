using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Api.Configuration.Mapper;

internal static class MapperExtension
{
    internal static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();

        return services;
    }
}