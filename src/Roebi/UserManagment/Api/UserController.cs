namespace Roebi.UserManagment.Api
{
    using BCrypt.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Roebi.Auth;
    using Roebi.Auth.Messages;
    using Roebi.Common.UnitOfWork;
    using Roebi.Helper;
    using Roebi.LogManagment.Domain;
    using Roebi.UserManagment.Domain;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IJwtUtils _jwtUtils;

        public UserController(IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            User user = _unitOfWork.User.Find(x => x.Username == model.Username).First();

            if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash)) {
                _unitOfWork.Log.Add(new Log($"User: {model.Username} login failed"));
                _unitOfWork.Save();
                throw new AppException("Username or password is incorrect");
            }

            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            _unitOfWork.Log.Add(new Log($"User: {user.Username} successful login"));
            _unitOfWork.Save();
            return Ok(new AuthenticateResponse(user, jwtToken));
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var currentUser = HttpContext.Items["User"] as User;
            _unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} loads all users"));
            _unitOfWork.Save();
            return Ok(_unitOfWork.User.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            // only admins can access other user records
            var currentUser = HttpContext.Items["User"] as User;
            if (id != currentUser?.Id && currentUser?.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = _unitOfWork.User.GetById(id);
            return Ok(user);
        }
    }
}
