using System.Security.Claims;

namespace ancanet.server.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserId(this HttpContext context) => context?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("No user id found!");
        public static string GetRole(this HttpContext context) => context?.User.FindFirstValue(ClaimTypes.Role) ?? throw new ArgumentNullException("No role Found!");
    }
}
