namespace CManager.API.Controllers
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

        [HttpPost]
        public async Task<Unit> Create([FromBody]AddTipoCombustivelCommand.TipoCombustivelCommand command) => await _mediatr.Send(command);

        [HttpGet("getAll")]
        public async Task<List<TipoCombustivelResponse>> GetAll([FromQuery] GetTiposCombustiveisQuery.TiposCombustiveis query) => await _mediatr.Send(query);

        [HttpGet]
        public async Task<TipoCombustivelResponse> Get([FromQuery] GetTipoCombustivelQuery.TipoCombustivel query) => await _mediatr.Send(query);
    }
}