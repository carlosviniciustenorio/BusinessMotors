namespace BusinessMotors.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VersoesController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public VersoesController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Cria uma nova versão.
        /// </summary>
        /// <remarks>
        /// Esta operação permite criar uma nova versão com base nos dados fornecidos no corpo da solicitação.
        /// </remarks>
        /// <param name="command">Os dados necessários para criar a versão.</param>
        /// <returns>Uma unidade indicando que a versão foi criada com sucesso.</returns>
        /// <response code="200">Retorna uma unidade se a versão for criada com sucesso.</response>
        /// <response code="500">Se ocorrer um erro interno do servidor.</response>
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<Unit> Create([FromBody]AddVersaoCommand.VersaoCommand command) => await _mediatr.Send(command);
    }
}