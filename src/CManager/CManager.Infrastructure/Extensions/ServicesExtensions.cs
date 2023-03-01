using CManager.Application.Services;
using CManager.Infrastructure.Context.CManager;
using CManager.Infrastructure.Context.Identity;
using CManager.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace CManager.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void RegisterDBServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(configuration.GetSection("ConnectionString").Value));
            services.AddDbContext<CManagerDBContext>(options => options.UseSqlServer(configuration.GetSection("ConnectionString").Value));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
