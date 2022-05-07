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
        public IEnumerable<Room> GetRooms()
        {
            return _unitOfWork.Room.GetAll();
        }

        [HttpGet("{id}")]
        public Room GetSingleRoom(int id) 
        {
            return _unitOfWork.Room.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Room room)
        {
            _unitOfWork.Room.Add(room);
            _unitOfWork.Save();
        }

        [HttpPut]
        public void Put(Room room) {
            _unitOfWork.Room.Update(room);
            _unitOfWork.Save();
        }
       
    }
}
