namespace CManager.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public MarcasController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Cria uma nova marca.
        /// </summary>
        /// <remarks>
        /// Esta operação permite criar uma nova marca com base nos dados fornecidos no corpo da solicitação.
        /// </remarks>
        /// <param name="command">Os dados necessários para criar a marca.</param>
        /// <returns>Uma unidade indicando que a marca foi criada com sucesso.</returns>
        /// <response code="200">Retorna uma unidade se a marca for criada com sucesso.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<Unit> Create([FromBody] AddMarcaCommand.MarcaCommand command) => await _mediatr.Send(command);

        /// <summary>
        /// Retorna todas as marcas cadastradas.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna uma lista de todas as marcas atualmente cadastradas no sistema.
        /// </remarks>
        /// <param name="query">Filtros opcionais para a consulta de marcas.</param>
        /// <returns>Uma lista de objetos MarcaResponse que representam as marcas.</returns>
        /// <response code="200">Retorna todas as marcas cadastradas.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(List<MarcaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getAll")]
        public async Task<List<MarcaResponse>> GetAll([FromQuery] GetMarcasQuery.Marcas query) => await _mediatr.Send(query);

        /// <summary>
        /// Retorna uma marca com base nos parâmetros fornecidos.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna uma única marca com base nos parâmetros fornecidos.
        /// </remarks>
        /// <param name="query">Os parâmetros necessários para a consulta da marca.</param>
        /// <returns>O objeto MarcaResponse que representa a marca solicitada.</returns>
        /// <response code="200">Retorna a marca com base nos parâmetros fornecidos.</response>
        /// <response code="404">Se a marca com o ID fornecido não for encontrada.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(MarcaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<MarcaResponse> Get([FromQuery] GetMarcaQuery.Marca query) => await _mediatr.Send(query);

    }
}