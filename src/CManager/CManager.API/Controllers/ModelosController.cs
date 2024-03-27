namespace CManager.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ModelosController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ModelosController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Cria um novo modelo.
        /// </summary>
        /// <remarks>
        /// Esta operação permite criar um novo modelo com base nos dados fornecidos no corpo da solicitação.
        /// </remarks>
        /// <param name="command">Os dados necessários para criar o modelo.</param>
        /// <returns>Uma unidade indicando que o modelo foi criado com sucesso.</returns>
        /// <response code="200">Retorna uma unidade se o modelo for criado com sucesso.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<Unit> Create([FromBody] AddModeloCommand.ModeloCommand command) => await _mediatr.Send(command);

        /// <summary>
        /// Retorna todos os modelos cadastrados.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna uma lista de todos os modelos atualmente cadastrados no sistema.
        /// </remarks>
        /// <param name="query">Filtros opcionais para a consulta de modelos.</param>
        /// <returns>Uma lista de objetos ModeloResponse que representam os modelos.</returns>
        /// <response code="200">Retorna todos os modelos cadastrados.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(List<ModeloResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getAll")]
        public async Task<List<ModeloResponse>> GetAll([FromQuery] GetModelosQuery.Modelos query) => await _mediatr.Send(query);

        /// <summary>
        /// Retorna um modelo com base nos parâmetros fornecidos.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna um único modelo com base nos parâmetros fornecidos.
        /// </remarks>
        /// <param name="query">Os parâmetros necessários para a consulta do modelo.</param>
        /// <returns>O objeto ModeloResponse que representa o modelo solicitado.</returns>
        /// <response code="200">Retorna o modelo com base nos parâmetros fornecidos.</response>
        /// <response code="404">Se o modelo com os parâmetros fornecidos não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(ModeloResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ModeloResponse> Get([FromQuery] GetModeloQuery.Modelo query) => await _mediatr.Send(query);

    }
}