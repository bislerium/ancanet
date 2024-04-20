namespace ancanet.server.Helpers
{
    public static class Url
    {
        public static string GetAbsoluteUrl(HttpContext context, string relativeUrl)
        {
            var request = context.Request;            
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = relativeUrl                
            };
            if (request.Host.Port.HasValue)
            {
                uriBuilder.Port = request.Host.Port.Value;
            }          

            return uriBuilder.Uri.ToString();
        }
    }
}
