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

        [HttpPost("create")]
        public async Task<Unit> Create([FromBody]AddModeloCommand.ModeloCommand command) => await _mediatr.Send(command);
    }
}