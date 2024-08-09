using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeoChecker.SEORankChecker;
using SeoChecker.SEORankChecker.Models;

namespace SeoChecker.Pages
{
    public class IndexModel : PageModel
    {
        //private HttpClient _client;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RankChecker _rankChecker;

        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string Keywords { get; set; }
        [BindProperty]
        public string Url { get; set; }
        public SearchResult SearchResult { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            RankChecker rankChecker
            //, IHttpClientFactory clientFactory
            //, IHttpContextAccessor httpContextAccessor
            )
        {
            _rankChecker = rankChecker;
            //_httpContextAccessor = httpContextAccessor;
            //_client = clientFactory.CreateClient();
            //_client.BaseAddress= new Uri("http://localhost:8080");
            _logger = logger;
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                SearchResult = await _rankChecker.PerformSearch("google", new SearchQuery(Keywords, Url));

            }

            return Page();
        }
        public void OnGet()
        {

        }

        //private string GetBasedURi()
        //{
        //    var request = _httpContextAccessor.HttpContext.Request;
        //    var scheme = request.Scheme; // e.g., "http" or "https"
        //    var host = request.Host; // e.g., "localhost:5001"
        //    var path = request.Path; // The request path

        //    return $"{scheme}://{host}";
        //}
    }
}
