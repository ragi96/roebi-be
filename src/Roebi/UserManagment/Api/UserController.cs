namespace Roebi.UserManagment.Api
{
    using BCrypt.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Roebi.Auth;
    using Roebi.Auth.Messages;
    using Roebi.Common.UnitOfWork;
    using Roebi.Helper;
    using Roebi.UserManagment.Domain;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private IJwtUtils _jwtUtils;

        public UserController(IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            this.unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            User user = unitOfWork.User.Find(x => x.Username == model.Username).First();

            if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return Ok(new AuthenticateResponse(user, jwtToken));
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(unitOfWork.User.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            // only admins can access other user records
            var currentUser = HttpContext.Items["User"] as User;
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = unitOfWork.User.GetById(id);
            return Ok(user);
        }
    }
}
