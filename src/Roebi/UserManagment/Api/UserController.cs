namespace Roebi.UserManagment.Api
{
    using BCrypt.Net;
    using Microsoft.AspNetCore.Mvc;
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
        private readonly IJwtUtils _jwtUtils;

        public UserController(IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
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
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var currentUser = HttpContext.Items["User"] as User;
            _unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} loads all users"));
            _unitOfWork.Save();
            return Ok(_unitOfWork.User.GetAll());
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        public IActionResult Put(User user)
        {
            var currentUser = HttpContext.Items["User"] as User;
            //var oldUser = _unitOfWork.User.GetById(user.Id);
            _unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} updates from {user} to {user}"));
            user.PasswordHash = BCrypt.HashPassword(user.PasswordHash);
            _unitOfWork.User.Update(user);
            return Ok(_unitOfWork.Save());
        }

        [HttpGet("{id:int}")]
        public ActionResult<User> GetById(int id)
        {
            // only admins can access other user records
            var currentUser = HttpContext.Items["User"] as User;
            if (id != currentUser?.Id && currentUser?.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            return Ok(_unitOfWork.User.GetById(id));
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            var currentUser = HttpContext.Items["User"] as User;
            var user = _unitOfWork.User.GetById(id);
            if (user != null) {
                _unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} deletes {user}"));
                _unitOfWork.User.Remove(user);
                _unitOfWork.Save();
            }
            return Ok();
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var currentUser = HttpContext.Items["User"] as User;
            user.PasswordHash = BCrypt.HashPassword(user.PasswordHash);
            _unitOfWork.User.Add(user);
            _unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} creates {user}"));
            _unitOfWork.Save();
            return Ok();
        }
    }
}
