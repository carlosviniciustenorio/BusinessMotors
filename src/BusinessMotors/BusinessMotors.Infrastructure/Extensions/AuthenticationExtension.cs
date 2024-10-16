using System.Reflection;
using System.Security.Cryptography;
using BusinessMotors.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BusinessMotors.Infrastructure.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration, JwtOptions jwtOptions)
        {
            var relativePublicKeyPath = jwtOptions.PublicKeyPath;
            var publicKeyPath = Path.Combine(Directory.GetCurrentDirectory(), relativePublicKeyPath);

            if (!File.Exists(publicKeyPath))
                throw new FileNotFoundException($"Public key file not found at path: {publicKeyPath}");

            string publicKeyContent = File.ReadAllText(publicKeyPath);
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKeyContent);

            // var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SecurityKey));
            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions.Issuer;
                options.Audience = jwtOptions.Audience;
                //Chave Simétrica
                // options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
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
                IssuerSigningKey = new RsaSecurityKey(rsa),

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
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = "601031446578-041rnfqa1p0lifpsju0vj7f3labjr15j.apps.googleusercontent.com";
                options.ClientSecret = "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";
                options.CallbackPath = "/api/usuarios/google-response";
                options.SaveTokens = true;
                options.SignInScheme = IdentityConstants.ExternalScheme;
            });
        }
    }
}
