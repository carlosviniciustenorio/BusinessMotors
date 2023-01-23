using System.Text.Json.Serialization;

namespace CManager.Infrastructure.Utils
{
    public class GitHubBranch
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
