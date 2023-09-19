using CManager.API.Attributes;
using CManager.API.Shared;
using CManager.Infrastructure.Constants.Identity;
using CManager.Integration.Cache;
using ECommerceCT.Application.DTOs.Requests;
using ECommerceCT.Application.DTOs.Responses;
using System.Net;
using System.Security.Claims;

namespace CManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<UsuariosController> _logger;
        public UsuariosController(IIdentityService identityService, ILogger<UsuariosController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        /// <summary>
        /// Cadastro de usuário.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="usuarioCadastro">Dados de cadastro do usuário</param>
        /// <returns></returns>
        /// <response code="200">Usuário criado com sucesso</response>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(UsuarioCadastroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = Roles.Admin)]
        [ClaimsAuthorizeAttribute(Infrastructure.Constants.Identity.ClaimTypes.Usuarios, "Create")]
        [HttpPost("cadastro")]
        public async Task<ActionResult<UsuarioCadastroResponse>> Cadastrar([FromBody] UsuarioCadastroRequest usuarioCadastro)
        {
            _logger.LogInformation($"Tentativa de cadastro de usuário, request: {JsonConvert.SerializeObject(usuarioCadastro)}");
            if (!ModelState.IsValid)
                return BadRequest();
        
            UsuarioCadastroResponse resultado = await _identityService.CadastrarUsuario(usuarioCadastro);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Any())
                return BadRequest(new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros));

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

            UsuarioLoginResponse resultado = await _identityService.Login(usuarioLogin);
            if (resultado.Sucesso)
                return Ok(resultado);

            return Unauthorized(resultado);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<UsuarioCadastroResponse>> Refresh()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = identity?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
                return BadRequest();

            var resultado = await _identityService.LoginComRefreshToken(usuarioId);
            if (resultado.Sucesso)
                return Ok(resultado);

            return Unauthorized();
        }
    }
}
