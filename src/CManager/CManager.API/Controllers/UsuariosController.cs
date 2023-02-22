using CManager.API.Attributes;
using CManager.Application.Services;
using CManager.Infrastructure.Constants.Identity;
using ECommerceCT.Application.DTOs.Requests;
using ECommerceCT.Application.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;

        public UsuariosController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [Authorize(Roles = Roles.Admin)]
        [ClaimsAuthorizeAttribute(ClaimTypes.Usuarios, "Create")]
        [HttpPost("cadastro")]
        public async Task<ActionResult<UsuarioCadastroResponse>> Cadastrar([FromBody] UsuarioCadastroRequest usuarioCadastro)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.CadastrarUsuario(usuarioCadastro);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
                return BadRequest(resultado);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("role")]
        public async Task<ActionResult> Role([FromBody] UsuarioCadastroRole user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _identityService.CadastrarRole(user.Role, user.Email);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioCadastroResponse>> Login(UsuarioLoginRequest usuarioLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.Login(usuarioLogin);
            if (resultado.Sucesso)
                return Ok(resultado);

            return Unauthorized(resultado);
        }
    }
}
