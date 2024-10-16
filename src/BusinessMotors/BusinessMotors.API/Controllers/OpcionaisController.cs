namespace BusinessMotors.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpcionaisController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public OpcionaisController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Cria um novo opcional.
        /// </summary>
        /// <remarks>
        /// Esta operação permite criar um novo opcional com base nos dados fornecidos no corpo da solicitação.
        /// </remarks>
        /// <param name="command">Os dados necessários para criar o opcional.</param>
        /// <returns>Uma unidade indicando que o opcional foi criado com sucesso.</returns>
        /// <response code="200">Retorna uma unidade se o opcional for criado com sucesso.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost]
        public async Task<Unit> Create([FromBody] AddOpcionalCommand.OpcionalCommand command) => await _mediatr.Send(command);

        /// <summary>
        /// Retorna todos os opcionais cadastrados.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna uma lista de todos os opcionais atualmente cadastrados no sistema.
        /// </remarks>
        /// <param name="query">Filtros opcionais para a consulta de opcionais.</param>
        /// <returns>Uma lista de objetos OpcionalResponse que representam os opcionais.</returns>
        /// <response code="200">Retorna todos os opcionais cadastrados.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(List<OpcionalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<List<OpcionalResponse>> Get([FromQuery] GetOpcionaisQuery.Opcionais query) => await _mediatr.Send(query);

        /// <summary>
        /// Retorna um opcional com base nos parâmetros fornecidos.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna um único opcional com base nos parâmetros fornecidos.
        /// </remarks>
        /// <param name="query">Os parâmetros necessários para a consulta do opcional.</param>
        /// <returns>O objeto OpcionalResponse que representa o opcional solicitado.</returns>
        /// <response code="200">Retorna o opcional com base nos parâmetros fornecidos.</response>
        /// <response code="404">Se o opcional com os parâmetros fornecidos não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(OpcionalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<OpcionalResponse> Get([FromRoute] GetOpcionalQuery.Opcional query) => await _mediatr.Send(query);

    }
}