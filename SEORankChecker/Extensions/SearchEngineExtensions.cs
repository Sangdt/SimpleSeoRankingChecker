using Microsoft.Extensions.DependencyInjection.Extensions;
using SeoChecker.Contracts.SearchEngine;
using SeoChecker.SEORankChecker;
using System.Reflection;

namespace SeoChecker.SEORankChecker.Extensions
{
    public static class SearchEngineExtensions
    {
        public static IServiceCollection AddSearchEngines(this IServiceCollection services)
        {
            services.AddSearchEngines(Assembly.GetExecutingAssembly());
            return services;
        }
        public static IServiceCollection AddSearchEngines(this IServiceCollection services, Assembly assembly)
        {
            ServiceDescriptor[] descriptors = assembly.DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(ISearchEngine)))
                .Select(type => new ServiceDescriptor(typeof(ISearchEngine), type, ServiceLifetime.Transient)).ToArray();
            services.TryAddEnumerable(descriptors);
            // adding factory
            services.TryAddSingleton<ISearchEngineFactory, SearchEngineFactory>();
            services.TryAddSingleton<RankChecker>();
            return services;
        }



    }
}
