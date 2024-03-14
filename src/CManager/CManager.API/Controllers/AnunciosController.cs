using System.Collections.ObjectModel;
using CManager.Integration.Clients;
using Nest;
using Sentry;

namespace CManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnunciosController : ControllerBase
    {
        private readonly IMediator _mediatr;
        
        public AnunciosController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Cadastra um anúncio
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna todas as categorias cadastradas</response>
        /// <response code="500">Retorna erros caso ocorram</response>
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Authorize]
        public async Task<Unit> Create([FromForm]AddAnuncioCommand.Command command)
        {
            if (string.IsNullOrEmpty(command.usuarioId))
            {
                var userId = User.Claims.FirstOrDefault(d => d.Type.Contains("nameidentifier")).Value; 
                command.usuarioId = userId;
            }

            await _mediatr.Send(command);    
            return Unit.Value;
        } 

        [HttpGet]
        public async Task<List<AnunciosResponse>> GetAll([FromQuery]GetAnunciosQuery.Anuncios command) => await _mediatr.Send(command);    
            
        [HttpGet("{id}")]
        public async Task<AnuncioResponse> GetById([FromQuery]GetAnuncioQuery.Anuncio command) => await _mediatr.Send(command);    
    }
}