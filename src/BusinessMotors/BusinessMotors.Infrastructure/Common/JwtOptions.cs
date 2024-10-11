using Microsoft.IdentityModel.Tokens;

namespace BusinessMotors.Infrastructure.Common
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
        public string PrivateKeyPath { get; set; }
    }
}
