namespace ancanet.server.Middlewares;

public class SetupRedirectionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (!(context.User.Identity?.IsAuthenticated ?? false))
        {
            await next(context);
            return;
        }

        if (!await ValidateProfileSetup(context)) return;

        await next(context);
    }

    private static async Task<bool> ValidateProfileSetup(HttpContext context)
    {
        var isProfileConfigured = context.User.HasClaim(c => c is
        {
            Type: AncanetConsts.Claims.IsProfileSetup.Type,
            Value: AncanetConsts.Claims.IsProfileSetup.True,
            ValueType: AncanetConsts.Claims.IsProfileSetup.ValueType
        });

        if (!isProfileConfigured)
        {
            context.Response.Headers.Location = AncanetConsts.ProfileSetupURL;
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(
                new
                {
                    Message = "Profile setup incomplete. Please complete your profile before using this feature.",
                    Code = "ProfileIncomplete",
                    Data = new
                    {
                        CompletionUrl = AncanetConsts.ProfileSetupURL
                    }
                });
            return false;
        }
        return true;
    }   
}

public static class ProfileConfigureRedirectionMiddlewareUsage
{
    public static IApplicationBuilder UseProfileConfigureRedirection(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SetupRedirectionMiddleware>();
    }
}