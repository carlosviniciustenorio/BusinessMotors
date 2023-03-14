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

        [HttpPost("create")]
        public async Task<Unit> Create([FromBody]AddTipoCombustivelCommand.TipoCombustivelCommand command) => await _mediatr.Send(command);
    }
}