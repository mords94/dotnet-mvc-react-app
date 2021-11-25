using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("guest")]
    public class GuestController : ControllerBase
    {

        [HttpGet]
        public NoContentResult Get()
        {
            return NoContent();
        }
    }
}
