namespace SolarWatch.ApiConnect
{
    public interface IWebClient
    {
        Task<string> DownloadStringAsync(string url);
    }
}
