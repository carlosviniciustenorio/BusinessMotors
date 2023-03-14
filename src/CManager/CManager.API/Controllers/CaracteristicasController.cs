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

        [HttpPost("create")]
        public async Task<Unit> Create([FromBody]AddCaracteristicaCommand.CaracteristicaCommand command) => await _mediatr.Send(command);
    }
}