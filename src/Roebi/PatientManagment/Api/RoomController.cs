using Microsoft.AspNetCore.Mvc;

namespace Roebi.RoomManagment.Api
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;

        public RoomController(ILogger<RoomController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRooms")]
        public string Get()
        {
            return "asdasd";
        }
    }
}
