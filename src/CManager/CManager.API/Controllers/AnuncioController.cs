namespace CManager.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AnuncioController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public AnuncioController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("create")]
        public async Task<Unit> Create([FromForm]AddAnuncioCommand.Command command)
        {
            if (string.IsNullOrEmpty(command.usuarioId))
            {
                var userId = User.Claims.FirstOrDefault(d => d.Type.Contains("nameidentifier")).Value;
                command.usuarioId = userId;
            }

            await _mediatr.Send(command);    
            return Unit.Value;
        } 

        [HttpGet("getAll")]
        public async Task<List<AnunciosResponse>> GetAll([FromQuery]GetAnunciosQuery.Anuncios command)
        {
            var response = await _mediatr.Send(command);    
            return response;
        } 

        [HttpGet]
        public async Task<AnuncioResponse> GetById([FromQuery]GetAnuncioQuery.Anuncio command)
        {
            var response = await _mediatr.Send(command);    
            return response;
        } 
    }
}