using Polly;
using System.Net.Http;

namespace BusinessMotors.Integration.Clients
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncPolicy _resiliencePolicy;

        public CatalogService(HttpClient httpClient, AsyncPolicy resiliencePolicy)
        {
            _httpClient = httpClient;
            _resiliencePolicy = resiliencePolicy;
        }

        public async Task<string> GetCatalogItems(int page, int take)
        {
            Console.WriteLine($"Base Adress: {_httpClient.BaseAddress}");
            
            var responseString = await _resiliencePolicy.ExecuteAsync<string>(() =>
            {
                return _httpClient.GetStringAsync(_httpClient.BaseAddress);
            });

            return responseString;
        }
    }

    public interface ICatalogService
    {
        Task<string> GetCatalogItems(int page, int take);
    }
}