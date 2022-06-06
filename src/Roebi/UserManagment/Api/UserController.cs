namespace Roebi.UserManagment.Api
{
    using System.Text.Json;
    using AutoMapper;
    using BCrypt.Net;
    using Microsoft.AspNetCore.Mvc;
    using Roebi.Auth;
    using Roebi.Auth.Messages;
    using Roebi.Common.UnitOfWork;
    using Roebi.Helper;
    using Roebi.LogManagment.Domain;
    using Roebi.UserManagment.Application.Dto;
    using Roebi.UserManagment.Domain;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtUtils _jwtUtils;
        public readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IJwtUtils jwtUtils, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            User? user = _unitOfWork.User.Find(x => x.Username == model.Username).FirstOrDefault();

            if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash)) {
                _unitOfWork.Log.Add(new Log($"User: {model.Username} login failed"));
                _unitOfWork.Save();
                return NotFound();
            }

            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            _unitOfWork.Log.Add(new Log($"User: {user.Username} successful login"));
            _unitOfWork.Save();
            return Ok(new AuthenticateResponse(user, jwtToken));
        }

        [Authorize(Role.Admin, Role.User, Role.Roboter)]
        [HttpGet("current")]
        public ActionResult<User> CurrentUser()
        {
            return Ok(HttpContext.Items["User"] as User);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_unitOfWork.User.GetAll());
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        public IActionResult Put(UpdateUserDto userDto)
        {
            if (userDto != null) {
                var activeUser = HttpContext.Items["User"] as User;
                User user = _unitOfWork.User.GetById(userDto.Id);
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Username = userDto.Username;
                user.Role = userDto.Role;
                _unitOfWork.Log.Add(new Log($"User: {activeUser.Username} updated user {user?.Id} to {JsonSerializer.Serialize<User>(user)}"));
                _unitOfWork.User.Update(user);
                return Ok(_unitOfWork.Save());
            } else {
                return BadRequest();
            }
        }

        [Authorize(Role.Admin)]
        [HttpPut("change-password")]
        public IActionResult ChangePassword(PasswordUpdate passwordRequest){
            if (passwordRequest != null) {
                var activeUser = HttpContext.Items["User"] as User;
                var user = _unitOfWork.User.GetById(passwordRequest.Id);
                user.PasswordHash = BCrypt.HashPassword(passwordRequest.Password);
                _unitOfWork.Log.Add(new Log($"User: {activeUser.Username} changed password of {user.Username}"));
                _unitOfWork.User.Update(user);
                return Ok(_unitOfWork.Save());
            } else {
                return BadRequest();
            }
        }

        [Authorize(Role.Admin)]
        [HttpPut("current-change-password")]
        public IActionResult ChangeCurrentPassword(PasswordCurrentUpdate passwordRequest)
        {
            if (passwordRequest != null)
            {
                var user = _unitOfWork.User.GetById(passwordRequest.Id);
                if (BCrypt.Verify(passwordRequest.OldPassword, user.PasswordHash))
                {
                    user.PasswordHash = BCrypt.HashPassword(passwordRequest.NewPassword);
                    _unitOfWork.Log.Add(new Log($"User: {user.Username} changed his password"));
                    _unitOfWork.User.Update(user);
                    return Ok(_unitOfWork.Save());
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Role.Admin, Role.User, Role.Roboter)]
        [HttpPut("current")]
        public IActionResult PutCurrentUser(UpdateCurrentUserDto user)
        {
            var activeUser = HttpContext.Items["User"] as User;
            if (activeUser.Id == user.Id && user != null) { 
                activeUser.FirstName = user.FirstName;
                activeUser.LastName = user.LastName;
                activeUser.Username = user.Username;
                _unitOfWork.Log.Add(new Log($"User: {activeUser?.Username} updated user {activeUser?.Id} to {JsonSerializer.Serialize<User>(activeUser)}"));
                _unitOfWork.User.Update(activeUser);
                return Ok(_unitOfWork.Save());
            } else {
                return BadRequest();
            }
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
                _unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} deletes {JsonSerializer.Serialize<User>(user)}"));
                _unitOfWork.User.Remove(user);
                _unitOfWork.Save();
            }
            return Ok();
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public IActionResult Post(AddUserDto userDto)
        {
            var currentUser = HttpContext.Items["User"] as User;
            userDto.PasswordHash = BCrypt.HashPassword(userDto.PasswordHash);
            User user = _mapper.Map<User>(userDto);
            _unitOfWork.User.Add(user);
            _unitOfWork.Log.Add(new Log($"User: {currentUser?.Username} creates {JsonSerializer.Serialize<User>(user)}"));
            _unitOfWork.Save();
            return Ok();
        }
    }
}
