using ancanet.server.Data;

namespace ancanet.server.Managers
{
    public class ProfileManager(AppDbContext appDbContext)
    {
        public Task AddProfile()
        {
            return Task.CompletedTask;
        }
    }
}
