namespace BusinessMotors.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TiposCombustiveisController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public TiposCombustiveisController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Cria um novo tipo de combustível.
        /// </summary>
        /// <remarks>
        /// Esta operação permite criar um novo tipo de combustível com base nos dados fornecidos no corpo da solicitação.
        /// </remarks>
        /// <param name="command">Os dados necessários para criar o tipo de combustível.</param>
        /// <returns>Uma unidade indicando que o tipo de combustível foi criado com sucesso.</returns>
        /// <response code="200">Retorna uma unidade se o tipo de combustível for criado com sucesso.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<Unit> Create([FromBody] AddTipoCombustivelCommand.TipoCombustivelCommand command) => await _mediatr.Send(command);

        /// <summary>
        /// Retorna todos os tipos de combustível cadastrados.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna uma lista de todos os tipos de combustível atualmente cadastrados no sistema.
        /// </remarks>
        /// <param name="query">Filtros opcionais para a consulta de tipos de combustível.</param>
        /// <returns>Uma lista de objetos TipoCombustivelResponse que representam os tipos de combustível.</returns>
        /// <response code="200">Retorna todos os tipos de combustível cadastrados.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(List<TipoCombustivelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<List<TipoCombustivelResponse>> Get([FromQuery] GetTiposCombustiveisQuery.TiposCombustiveis query) => await _mediatr.Send(query);

        /// <summary>
        /// Retorna um tipo de combustível com base nos parâmetros fornecidos.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna um único tipo de combustível com base nos parâmetros fornecidos.
        /// </remarks>
        /// <param name="query">Os parâmetros necessários para a consulta do tipo de combustível.</param>
        /// <returns>O objeto TipoCombustivelResponse que representa o tipo de combustível solicitado.</returns>
        /// <response code="200">Retorna o tipo de combustível com base nos parâmetros fornecidos.</response>
        /// <response code="404">Se o tipo de combustível com os parâmetros fornecidos não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(TipoCombustivelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<TipoCombustivelResponse> Get([FromRoute] GetTipoCombustivelQuery.TipoCombustivel query) => await _mediatr.Send(query);


    }
}