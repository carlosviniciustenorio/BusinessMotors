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

        [HttpPost]
        public async Task<Unit> Create([FromBody]AddCaracteristicaCommand.CaracteristicaCommand command) => await _mediatr.Send(command);

        [HttpGet("getAll")]
        public async Task<List<CaracteristicaResponse>> GetAll([FromQuery] GetCaracteristicasQuery.Caracteristicas query) => await _mediatr.Send(query);

        [HttpGet]
        public async Task<CaracteristicaResponse> Get([FromQuery] GetCaracteristicaQuery.Caracteristica query) => await _mediatr.Send(query);
    }
}