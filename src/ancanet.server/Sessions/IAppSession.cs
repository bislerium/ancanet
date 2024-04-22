namespace ancanet.server.Sessions
{
    public interface IAppSession
    {
        public string GetUserId();
        public string GetRole();
    }
}
