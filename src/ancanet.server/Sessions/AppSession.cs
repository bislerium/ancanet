using ancanet.server.Extensions;
using System.Security.Claims;

namespace ancanet.server.Sessions
{
    public class AppSession(IHttpContextAccessor httpContextAccessor): IAppSession
    {

        private readonly HttpContext _httpContext = httpContextAccessor?.HttpContext ?? throw new ArgumentNullException("No authentic session!");



        private string user => httpContextAccessor.HttpContext?.GetUserId() ?? throw new ArgumentNullException("No authentic session!");

        public string GetRole()
        {
            throw new NotImplementedException();
        }

        string IAppSession.GetUserId()
        {
            throw new NotImplementedException();
        }
    }
}
