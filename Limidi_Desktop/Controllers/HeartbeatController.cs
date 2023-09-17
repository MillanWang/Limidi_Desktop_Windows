using Microsoft.AspNetCore.Mvc;

namespace Limidi_Desktop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HeartbeatController : ControllerBase
    {
        public HeartbeatController() { }

        [HttpGet(Name = "GetHeartbeat")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult GetHeartbeat()
        {
            return Ok("Heartbeat at: " + DateTime.Now);
        }
    }
}
