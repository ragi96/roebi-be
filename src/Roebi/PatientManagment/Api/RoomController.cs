using Microsoft.AspNetCore.Mvc;
using Roebi.Common.UnitOfWork;
using Roebi.Auth;
using Roebi.PatientManagment.Domain;
using Roebi.UserManagment.Domain;

namespace Roebi.RoomManagment.Api
{

    [Authorize(Role.Admin, Role.User)]
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRooms()
        {
            return Ok(_unitOfWork.Room.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetSingleRoom(int id) 
        {
            return Ok(_unitOfWork.Room.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Room room)
        {
            _unitOfWork.Room.Add(room);
            return Ok(_unitOfWork.Save());
        }

        [HttpPut]
        public IActionResult Put(Room room) {
            _unitOfWork.Room.Update(room);
            return Ok(_unitOfWork.Save());
        }
       
    }
}
