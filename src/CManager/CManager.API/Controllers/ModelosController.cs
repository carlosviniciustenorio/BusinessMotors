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

        [HttpPost]
        public async Task<Unit> Create([FromBody]AddModeloCommand.ModeloCommand command) => await _mediatr.Send(command);

        [HttpGet("getAll")]
        public async Task<List<ModeloResponse>> GetAll([FromQuery] GetModelosQuery.Modelos query) => await _mediatr.Send(query);

        [HttpGet]
        public async Task<ModeloResponse> Get([FromQuery] GetModeloQuery.Modelo query) => await _mediatr.Send(query);
    }
}