using Microsoft.Extensions.Options;
using Roebi.Common.UnitOfWork;
using Roebi.Helper;

namespace Roebi.Auth
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                //context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
