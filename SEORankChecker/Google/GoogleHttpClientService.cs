using Microsoft.AspNetCore.Mvc.RazorPages;
using SeoChecker.SEORankChecker.Models;

namespace SeoChecker.SEORankChecker.Google
{
    public sealed class GoogleHttpClientService
    {
        private readonly HttpClient _httpClient;
        private const string _googleSearchUrl = "search?q=";///
        public GoogleHttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //should return just the html string of the page
        public async Task<string> GetSearchPageResults(SearchQuery query, int pageNo)
        {
            var ggResponse = await _httpClient.GetAsync($"{_googleSearchUrl}{query.Keywords}&start={pageNo * 10}");
            if(ggResponse.IsSuccessStatusCode == false)
            {
               return string.Empty;
            }
            return await ggResponse.Content.ReadAsStringAsync();
        }
    }
}
