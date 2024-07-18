using Microsoft.AspNetCore.Mvc;

namespace CManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnunciosController : ControllerBase
    {
        private readonly CarService.CarServiceClient _grpcClient;
        
        public AnunciosController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        
}