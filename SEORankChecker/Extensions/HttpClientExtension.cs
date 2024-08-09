using Microsoft.Extensions.Options;
using SeoChecker.SEORankChecker.Google;
using SeoChecker.Settings;

namespace SeoChecker.SEORankChecker.Extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpCustomClient(this IServiceCollection services)
        {
            string UA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3";

            services.AddHttpClient<GoogleHttpClientService>((serviceProvider, httpClient) =>
            {
                var settting=serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
                httpClient.BaseAddress = new Uri("https://www.google.com/");
                httpClient.DefaultRequestHeaders.Add("User-Agent", settting.UA);
            });
            return services;
        }
    }
}
