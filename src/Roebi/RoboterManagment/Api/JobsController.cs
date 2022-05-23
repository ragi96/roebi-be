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
    public class JobsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Role.Admin, Role.Roboter)]
        [HttpGet]
        public ActionResult<IEnumerable<RoboterLog>> GetJobs()
        {
            var acutalTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            return Ok(_unitOfWork.Medication.Find(medication => medication.TakingStamp > acutalTimestamp));
        }
    }
}
