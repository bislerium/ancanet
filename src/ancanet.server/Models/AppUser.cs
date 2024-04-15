using Microsoft.AspNetCore.Identity;

namespace ancanet.server.Models
{
    public sealed class AppUser : IdentityUser<Guid>
    {
        // Additional Columns for User Table
        
        public bool IsProfileConfigured { get; set; } 
    }
}
