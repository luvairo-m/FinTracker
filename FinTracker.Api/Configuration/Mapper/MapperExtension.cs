using System;
using System.Linq;
using AutoMapper;
using FinTracker.Api.Controllers.Bill.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace FinTracker.Api.Configuration.Mapper;

internal static class MapperExtension
{
    internal static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(configuration =>
        {
            var profiles = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(Profile).IsAssignableFrom(type) 
                               && !type.IsAbstract 
                               && !type.IsGenericTypeDefinition
                               && type.IsClass 
                               && type != typeof(Profile));
            
            foreach (var profile in profiles)
            {
                var profileInstance = (Profile)Activator.CreateInstance(profile);
                configuration.AddProfile(profileInstance);
            }
        });
        
        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
        
        return services;
    }
}