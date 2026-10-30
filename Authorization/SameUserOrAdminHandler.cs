using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace work_10.Authorization
{
    public class SameUserOrAdminHandler : AuthorizationHandler<SameUserOrAdminRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SameUserOrAdminHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SameUserOrAdminRequirement requirement)
        {
            if (context.User.Identity is not { IsAuthenticated: true })
                return Task.CompletedTask;

            var roleId = context.User.FindFirst(ClaimTypes.Role)?.Value;
            if (roleId == "1")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var currentUserId))
                return Task.CompletedTask;

            var httpContext = _httpContextAccessor.HttpContext;
            var routeValues = httpContext?.Request.RouteValues;

            if (routeValues != null && routeValues.TryGetValue("id", out var routeIdValue))
            {
                if (int.TryParse(routeIdValue?.ToString(), out var targetId))
                {
                    if (currentUserId == targetId)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}