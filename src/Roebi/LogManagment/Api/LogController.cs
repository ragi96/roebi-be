using Microsoft.AspNetCore.Mvc;
using Roebi.Auth;
using Roebi.Common.UnitOfWork;
using Roebi.LogManagment.Domain;
using Roebi.UserManagment.Domain;

namespace Roebi.LogManagment.Api
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Role.Admin)]
    public class LogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Log>> GetAll()
        {
            return Ok(_unitOfWork.Log.GetAll());
        }
    }
}
