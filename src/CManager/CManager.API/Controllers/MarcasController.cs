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

        [HttpPost]
        public async Task<Unit> Create([FromBody]AddMarcaCommand.MarcaCommand command) => await _mediatr.Send(command);

        [HttpGet("getAll")]
        public async Task<List<MarcaResponse>> GetAll([FromQuery] GetMarcasQuery.Marcas query) => await _mediatr.Send(query);

        [HttpGet]
        public async Task<MarcaResponse> Get([FromQuery] GetMarcaQuery.Marca query) => await _mediatr.Send(query);
    }
}