using Microsoft.AspNetCore.Mvc;
using Roebi.Common.UnitOfWork;
using Roebi.PatientManagment.Domain;

namespace Roebi.RoomManagment.Api
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public RoomController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Room> GetRooms()
        {
            return unitOfWork.Room.GetAll();
        }
    }
}
