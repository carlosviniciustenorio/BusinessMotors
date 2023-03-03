using CManager.Application.DTOs.Responses;

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
        public async Task<Unit> Create([FromBody]AddAnuncioCommand.Command command)
        {
            await _mediatr.Send(command);    
            return Unit.Value;
        } 

        [HttpGet("getAll")]
        public async Task<List<AnunciosResponse>> Get([FromQuery]GetAnunciosQuery.Anuncios command)
        {
            var response = await _mediatr.Send(command);    
            return response;
        } 
    }
}