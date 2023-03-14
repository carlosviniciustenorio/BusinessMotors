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

        [HttpPost("create")]
        public async Task<Unit> Create([FromBody]AddOpcionalCommand.OpcionalCommand command) => await _mediatr.Send(command);
    }
}