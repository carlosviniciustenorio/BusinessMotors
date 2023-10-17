namespace CManager.API.Controllers
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

        [HttpPost]
        public async Task<Unit> Create([FromBody]AddVersaoCommand.VersaoCommand command) => await _mediatr.Send(command);
    }
}