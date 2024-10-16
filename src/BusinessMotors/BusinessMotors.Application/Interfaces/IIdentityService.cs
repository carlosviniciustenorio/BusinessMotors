using ECommerceCT.Application.DTOs.Requests;
using ECommerceCT.Application.DTOs.Responses;

namespace BusinessMotors.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro, string provider = "");
        Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);
        Task CadastrarRole(string role, string email);
        Task<UsuarioLoginResponse> LoginRefreshToken(string usuarioId);
        Task<UsuarioDetalhesResponse> GetUserDetailsAsync(string id);
        Task<List<UsuarioResponse>> GetUsuariosAsync(string role);
        Task<Usuario> GetUserByEmailAsync(string email);
        Task<UsuarioLoginResponse> GerarCredenciais(string email);
    }
}
