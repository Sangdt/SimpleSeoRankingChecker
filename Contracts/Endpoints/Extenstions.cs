using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace SeoChecker.Contracts.Endpoints
{
    public static class Extenstions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            services.AddEndpoints(Assembly.GetExecutingAssembly());
            return services;
        }
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            ServiceDescriptor[] descriptors = assembly.DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => new ServiceDescriptor(typeof(IEndpoint), type, ServiceLifetime.Transient)).ToArray();
            services.TryAddEnumerable(descriptors);
            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
            IEndpointRouteBuilder endpointRouteBuilder = routeGroupBuilder is null ? app : routeGroupBuilder;

            foreach (var item in endpoints)
            {
                item.MapEndpoints(endpointRouteBuilder);
            }
            return app;
        }


    }
}
