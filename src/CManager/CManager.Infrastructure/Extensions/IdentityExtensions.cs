using CManager.Application.Services;
using CManager.Infrastructure.Services;

namespace CManager.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddEntityDependencies(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }
    }
}
