using BusinessMotors.Infrastructure.Common;
using ECommerceCT.Application.DTOs.Requests;
using ECommerceCT.Application.DTOs.Responses;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessMotors.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<Usuario> signInManager,
                               UserManager<Usuario> userManager,
                               IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest request)
        {
            var usuario = new Usuario
            {
                Nome = request.Nome,
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(usuario, request.Senha);
            var usuarioCadastroResponse = new UsuarioCadastroResponse(result.Succeeded);

            if (!result.Succeeded && result.Errors.Count() > 0)
            {
                usuarioCadastroResponse.AdicionarErros(result.Errors.Select(r => r.Description));
                return usuarioCadastroResponse;
            }
            
            await _userManager.SetLockoutEnabledAsync(usuario, false);
            await _userManager.AddToRoleAsync(usuario, request.Role);

            return usuarioCadastroResponse;
        }

        public async Task CadastrarRole(string role, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new InvalidOperationException("Usuário informado não encontrado");

            await _userManager.AddToRoleAsync(user, role.ToLower());
        }

        public async Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, false);
            if (result.Succeeded)
                return await GerarCredenciais(usuarioLogin.Email);

            UsuarioLoginResponse usuarioLoginResponse = new();
            if (result.IsLockedOut)
                usuarioLoginResponse.AdicionarErro("Essa conta está bloqueada");
            else if (result.IsNotAllowed)
                usuarioLoginResponse.AdicionarErro("Essa conta não tem permissão para fazer login");
            else if (result.RequiresTwoFactor)
                usuarioLoginResponse.AdicionarErro("É necessário confirmar o login no seu segundo fator de autenticação");
            else
                usuarioLoginResponse.AdicionarErro("Usuário ou senha estão incorretos");

            return usuarioLoginResponse;
        }

        public async Task<UsuarioLoginResponse> LoginComRefreshToken(string usuarioId)
        {
            var usuarioLoginResponse = new UsuarioLoginResponse();
            var usuario = await _userManager.FindByIdAsync(usuarioId);

            if (await _userManager.IsLockedOutAsync(usuario))
                usuarioLoginResponse.AdicionarErro("Conta está bloqueada");
            else if (!await _userManager.IsEmailConfirmedAsync(usuario))
                usuarioLoginResponse.AdicionarErro("Confirme o seu e-mail antes de realizar o login");

            if (usuarioLoginResponse.Sucesso)
                return await GerarCredenciais(usuario.Email);



            return usuarioLoginResponse;
        }

        private async Task<UsuarioLoginResponse> GerarCredenciais(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var accessTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: true);
            var refreshTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: false);

            var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
            var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

            var accessToken = GerarToken(accessTokenClaims, dataExpiracaoAccessToken);
            var refreshToken = GerarToken(refreshTokenClaims, dataExpiracaoRefreshToken);

            return new UsuarioLoginResponse
            (
                sucesso: true,
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        public string GerarToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IList<Claim>> ObterClaims(Usuario user, bool adicionarClaimsUsuario)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString()));

            if (adicionarClaimsUsuario)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }

        public async Task<UsuarioDetalhesResponse> GetDetailsUsuarioAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                throw new InvalidDataException();

            return new UsuarioDetalhesResponse(user.PhoneNumber, user.Email, user.Instagram, user.Twitter, user.Facebook);
        }
        
        public async Task<List<UsuarioResponse>> GetUsuariosAsync(string role)
        {
            var users = _userManager.GetUsersInRoleAsync(role).Result.ToList();
            List<UsuarioResponse> response = new();
            
            if (users is null && !users.Any())
                return response;

            users.ToList().ForEach(d => response.Add(new(d.Id, d.UserName)));
            return response;
        }
    }
}
