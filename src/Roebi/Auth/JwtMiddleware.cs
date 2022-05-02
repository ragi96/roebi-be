using Microsoft.Extensions.Options;
using Roebi.Common.UnitOfWork;
using Roebi.Helper;

namespace Roebi.Auth
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                context.Items["User"] = unitOfWork.User.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
