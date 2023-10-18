namespace CManager.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OpcionaisController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public OpcionaisController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<Unit> Create([FromBody]AddOpcionalCommand.OpcionalCommand command) => await _mediatr.Send(command);

        [HttpGet("getAll")]
        public async Task<List<OpcionalResponse>> GetAll([FromQuery] GetOpcionaisQuery.Opcionais query) => await _mediatr.Send(query);

        [HttpGet]
        public async Task<OpcionalResponse> Get([FromQuery] GetOpcionalQuery.Opcional query) => await _mediatr.Send(query);
    }
}