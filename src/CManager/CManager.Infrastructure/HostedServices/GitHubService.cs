using CManager.Infrastructure.Utils;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

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
