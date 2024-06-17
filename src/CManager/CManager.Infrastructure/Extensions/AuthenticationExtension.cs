using CManager.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CManager.Infrastructure.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration, JwtOptions jwtOptions)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SecurityKey));

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions.Issuer;
                options.Audience = jwtOptions.Audience;
                options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                options.AccessTokenExpiration = jwtOptions.AccessTokenExpiration;
                options.RefreshTokenExpiration = jwtOptions.RefreshTokenExpiration;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        throw context.Exception;
                    }
                };
            });
        }
    }
}
