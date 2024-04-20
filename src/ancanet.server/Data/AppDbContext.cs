using ancanet.server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ancanet.server.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :
            IdentityDbContext<AppUser>(options)
    {
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(AncanetConsts.Assembly);
            base.OnModelCreating(builder);
        }
    }
}
