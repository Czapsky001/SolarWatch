
using SolarWatch.Models;

namespace SolarWatch.ApiConnect
{
    public class WebClient : IWebClient
    {
        private readonly HttpClient _httpClient;
        public WebClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> DownloadStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
    }
}
