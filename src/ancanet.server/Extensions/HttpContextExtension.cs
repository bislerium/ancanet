using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace ancanet.server.Extensions
{
    public static class HttpContextExtension
    {
        public static bool IsUserAuthenticated(this HttpContext context) => context.User?.Identity?.IsAuthenticated ?? false;

        public static bool TryGetUserId(this HttpContext context, [NotNullWhen(true)] out string? userId) => (userId = context?.User.FindFirstValue(ClaimTypes.NameIdentifier)) is not null;
        public static string GetUserId(this HttpContext context) => context.TryGetUserId(out var userId) ? userId : throw new ArgumentNullException(nameof(userId));

        public static bool TryGetRoleId(this HttpContext context, [NotNullWhen(true)] out string? role) => (role = context?.User.FindFirstValue(ClaimTypes.Role)) is not null;
        public static string GetRoleId(this HttpContext context) => context.TryGetRoleId(out var roleId) ? roleId : throw new ArgumentNullException(nameof(roleId));
    }
}
