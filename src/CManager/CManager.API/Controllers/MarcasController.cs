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

        [HttpPost("create")]
        public async Task<Unit> Create([FromBody]AddMarcaCommand.MarcaCommand command) => await _mediatr.Send(command);
    }
}