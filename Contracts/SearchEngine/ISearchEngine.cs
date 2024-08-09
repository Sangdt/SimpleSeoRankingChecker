using SeoChecker.SEORankChecker.Models;

namespace SeoChecker.Contracts.SearchEngine
{
    public interface ISearchEngine
    {
        Task<SearchResult> GetSearchResults(SearchQuery query);
    }
}
