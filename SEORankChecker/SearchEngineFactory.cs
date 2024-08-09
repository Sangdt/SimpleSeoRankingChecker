using Microsoft.Extensions.DependencyInjection;
using SeoChecker.Contracts.SearchEngine;
using SeoChecker.SEORankChecker.Google;

namespace SeoChecker.SEORankChecker
{
    public interface ISearchEngineFactory
    {
        ISearchEngine GetSearchEngine(string engineName);
    }
    public class SearchEngineFactory : ISearchEngineFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SearchEngineFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchEngine GetSearchEngine(string engineName)
        {
            var services= _serviceProvider.GetServices<ISearchEngine>();
            return engineName.ToLower() switch
            {
                "google" => services.First(type=>type is GoogleRankChecker),
                //"bing" => _serviceProvider.GetRequiredService<BingRankChecker>(),
                _ => throw new NotSupportedException($"Search engine '{engineName}' is not supported.")
            };
        }
    }
}
