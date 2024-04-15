using ancanet.server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ancanet.server.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :
            IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
    {
        
    }

    
}
