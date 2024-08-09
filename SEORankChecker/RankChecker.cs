using Microsoft.Extensions.Caching.Memory;
using SeoChecker.SEORankChecker.Models;

namespace SeoChecker.SEORankChecker
{
    public class RankChecker
    {
        private readonly ISearchEngineFactory _searchEngineFactory;
        private readonly IMemoryCache _cache;

        public RankChecker(ISearchEngineFactory searchEngineFactory
            , IMemoryCache cache)
        {
            _searchEngineFactory = searchEngineFactory;
            _cache = cache;
        }

        public async Task<SearchResult> PerformSearch(string engineName, SearchQuery query)
        {
            string cacheKey = $"{engineName}:{query.Keywords}:{query.Url}";
            if (_cache.TryGetValue(cacheKey, out SearchResult cachedResult))
            {
                return cachedResult;
            }
            var seacher = _searchEngineFactory.GetSearchEngine(engineName);
            var result = await seacher.GetSearchResults(query);
            // 1 hour
            _cache.Set(cacheKey, result, TimeSpan.FromHours(1));
            return result;
        }
    }
}
