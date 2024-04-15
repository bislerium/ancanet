using System.Security.Claims;

namespace ancanet.server.Middlewares;

public class ProfileConfigureRedirectionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var isProfileConfigured = context.User.HasClaim(c => 
                c is 
                { 
                    Type: AncanetConsts.Claims.IsProfileConfigured.Type, 
                    Value: AncanetConsts.Claims.IsProfileConfigured.True, 
                    ValueType: AncanetConsts.Claims.IsProfileConfigured.ValueType 
                });

            if (isProfileConfigured)
            {
                await next(context);
            }
            else
            {
                context.Response.Redirect("/somewhere");
            }
        }
        else
        {
            await next(context);
        }
    }
}

public static class ProfileConfigureRedirectionMiddlewareUsage
{
    public static IApplicationBuilder UseProfileConfigureRedirection(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ProfileConfigureRedirectionMiddleware>();
    }
}