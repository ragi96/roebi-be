using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roebi.Auth;
using Roebi.Common.UnitOfWork;
using Roebi.RoboterManagment.Domain;
using Roebi.UserManagment.Domain;

namespace Roebi.RoboterManagment.Api
{
    [Route("[controller]")]
    [ApiController]
    public class RoboterLogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoboterLogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<RoboterLog>> GetAll()
        {
            return Ok(_unitOfWork.RoboterLog.GetAll());
        }

        [Authorize(Role.Roboter)]
        [HttpPost]
        public IActionResult Post([FromBody] RoboterLog log)
        {
            _unitOfWork.RoboterLog.Add(log);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
