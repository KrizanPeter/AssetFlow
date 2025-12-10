using AssetFlow.Shared.Contexts;
using Microsoft.AspNetCore.Http;

namespace AssetFlow.Shared.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IUserContext userContext)
        {
            var user = httpContext.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst("urn:assetflow:usser_Id")?.Value;
                var accountId = user.FindFirst("urn:assetflow:account_Id")?.Value;

                userContext.UserId = userId ?? string.Empty;
                userContext.AccountId = accountId ?? string.Empty; 
            }

            await _next(httpContext);
        }
    }
}

