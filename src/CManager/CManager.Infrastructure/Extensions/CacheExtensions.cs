using CManager.Integration.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace CManager.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddCacheDependency(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
