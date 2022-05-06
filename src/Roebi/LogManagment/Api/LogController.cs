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
        private readonly IUnitOfWork unitOfWork;

        public LogController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var currentUser = HttpContext.Items["User"] as User;
            unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} loads all Logs"));
            unitOfWork.Save();
            return Ok(unitOfWork.Log.GetAll());
        }
    }
}
