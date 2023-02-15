﻿namespace CManager.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddEntityDependencies(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            //services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
