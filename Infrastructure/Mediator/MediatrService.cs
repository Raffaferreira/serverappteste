using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace ApiTeste.Infrastructure.Mediator
{
    public static class MediatrService
    {
        public static void AddMediatrService(this IServiceCollection services)
        {
            const string applicationAssemblyName = "ApiTeste";
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            services.AddMediatR(new Assembly[] { assembly });
        }
    }
}
