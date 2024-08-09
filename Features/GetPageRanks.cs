using SeoChecker.Contracts.Endpoints;
using SeoChecker.SEORankChecker;
using SeoChecker.SEORankChecker.Models;

namespace SeoChecker.Features
{
    public class GetPageRanks : IEndpoint
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
           app.MapPost("api/pageranks/Google", GooglePageRankHandler);

        }
        public static async Task<IResult> GooglePageRankHandler(SearchQuery query,RankChecker rankChecker)
        {
            var result = await rankChecker.PerformSearch("google", query);

            if (result.IsUrlFound)
            {
                return Results.Ok(result.Positions);
            }
            else
            {
                return Results.NotFound("URL not found in the top 100 results.");
            }
        }
    }
}
