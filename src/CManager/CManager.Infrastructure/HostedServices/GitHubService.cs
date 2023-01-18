using CManager.Infrastructure.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CManager.Infrastructure.HostedServices
{
    public sealed class GitHubService : IHostedService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient) => (_httpClient) = (httpClient);

        public async Task<IEnumerable<GitHubBranch>?> GetCManagerBranchesAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<GitHubBranch>>(
                "repos/carlosviniciustenorio/CManager/branches");

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "pplication/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
            _httpClient.Timeout = TimeSpan.FromSeconds(15);

            var result = await GetCManagerBranchesAsync();
            result?.ToList().ForEach(branch => Console.WriteLine($"Name of branch: {branch.Name}"));
            Console.ReadLine();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
