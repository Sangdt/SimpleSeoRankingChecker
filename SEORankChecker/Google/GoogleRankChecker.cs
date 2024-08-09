using SeoChecker.Contracts.SearchEngine;
using SeoChecker.SEORankChecker.Models;

namespace SeoChecker.SEORankChecker.Google
{
    public class GoogleRankChecker : ISearchEngine
    {
        private readonly GoogleHttpClientService _client;
        private readonly int maxPage = 10;
        public GoogleRankChecker(GoogleHttpClientService client)
        {
            _client = client;
        }

        public async Task<SearchResult> GetSearchResults(SearchQuery query)
        {
            int pageNo = 1;
            var results = new SearchResult(query.Url);
            while (pageNo <= maxPage)
            {
                var htmlResult = await _client.GetSearchPageResults(query, pageNo);
                if (!string.IsNullOrEmpty(htmlResult))
                {
                    var result = GetPositioninResponse(htmlResult, query.Url);
                    if (result > 0)
                    {
                        results.AddPosition(result);
                    }
                    pageNo++;
                    await Task.Delay(5000);
                    continue;
                };
                break;
            }
            return results;
        }

        private int GetPositioninResponse(string htmlContent, string targetUrl)
        {
            // Simplified parsing logic (consider using a proper HTML parser for production)
            int position = 0;
            string[] searchResults = htmlContent.Split("<a href=\"");

            foreach (string resultBlock in searchResults)
            {
                if (resultBlock.Contains(targetUrl))
                {
                    position++;
                }
            }
         
            return position;
        }
    }
}
