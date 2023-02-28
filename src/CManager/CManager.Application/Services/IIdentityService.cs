using ECommerceCT.Application.DTOs.Requests;
using ECommerceCT.Application.DTOs.Responses;

namespace CManager.Application.Services
{
    public interface IIdentityService
    {
        Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);
        Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);
        Task CadastrarRole(string role, string email);
        Task<UsuarioLoginResponse> LoginComRefreshToken(string usuarioId);
    }
}
