using ObserverFire.Abstractions;
using ObserverFire.Common;

namespace ObserverFire.Api.Middlewares
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public CustomAuthorizationMiddleware(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/user"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("api-key", out var token))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Authorization header is missing.");
                return;
            }

            var apiKey = context.Request.Headers["api-key"].ToString();

            var user = await _userService.GetUserByApiKey(apiKey);

            if (user == null)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid ApiKey.");
                return;
            }
            else if (user.Role != RoleConstant.Admin)
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("You don't have permission to access this resource.");
                return;
            }
            else
            {
                await _next(context);
            }
        }
    }
}