using Microsoft.AspNetCore.Identity;

namespace ancanet.server.Models
{
    public sealed class AppUser : IdentityUser
    {
        public bool IsProfileSetup { get; set; }

        public UserProfile? UserProfile { get; set; }
    }
}
