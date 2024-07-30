namespace CManager.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CaracteristicasController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public CaracteristicasController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Cria uma nova característica.
        /// </summary>
        /// <remarks>
        /// Esta operação permite criar uma nova característica com base nos dados fornecidos no corpo da solicitação.
        /// </remarks>
        /// <param name="command">Os dados necessários para criar a característica.</param>
        /// <returns>Uma unidade indicando que a característica foi criada com sucesso.</returns>
        /// <response code="200">Retorna uma unidade se a característica for criada com sucesso.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<Unit> Create([FromBody] AddCaracteristicaCommand.CaracteristicaCommand command) => await _mediatr.Send(command);

        /// <summary>
        /// Retorna todas as características cadastradas.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna uma lista de todas as características atualmente cadastradas no sistema.
        /// </remarks>
        /// <param name="query">Filtros opcionais para a consulta de características.</param>
        /// <returns>Uma lista de objetos CaracteristicaResponse que representam as características.</returns>
        /// <response code="200">Retorna todas as características cadastradas.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(List<CaracteristicaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<List<CaracteristicaResponse>> Get([FromQuery] GetCaracteristicasQuery.Caracteristicas query) => await _mediatr.Send(query);

        /// <summary>
        /// Retorna uma característica com base nos parâmetros fornecidos.
        /// </summary>
        /// <remarks>
        /// Esta operação retorna uma única característica com base nos parâmetros fornecidos.
        /// </remarks>
        /// <param name="query">Os parâmetros necessários para a consulta da característica.</param>
        /// <returns>O objeto CaracteristicaResponse que representa a característica solicitada.</returns>
        /// <response code="200">Retorna a característica com base nos parâmetros fornecidos.</response>
        /// <response code="404">Se a característica com o ID fornecido não for encontrada.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(CaracteristicaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<CaracteristicaResponse> Get([FromRoute] GetCaracteristicaQuery.Caracteristica query) => await _mediatr.Send(query);

    }
}