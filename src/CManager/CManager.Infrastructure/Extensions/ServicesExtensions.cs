using CManager.Application.Services;
using CManager.Infrastructure.Context.Identity;
using CManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CManager.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("ECommerceConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
