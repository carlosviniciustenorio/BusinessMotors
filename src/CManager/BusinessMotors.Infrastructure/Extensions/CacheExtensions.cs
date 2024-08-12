namespace BusinessMotors.Infrastructure.Extensions
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
