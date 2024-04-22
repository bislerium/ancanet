using ancanet.server.Data;
using ancanet.server.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ancanet.server.Middlewares;

public class ProfileSetupRedirectionMiddleware(AppDbContext dbContext) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!(context.User.Identity?.IsAuthenticated ?? false)
            || AncanetConsts.PathExclusionsFromProfileSetup.Any(x => context.Request.Path.StartsWithSegments(x, StringComparison.OrdinalIgnoreCase)))
        {
            await next(context);
            return;
        }

        var userId = context.GetUserId();
        var isProfileSetup = await dbContext.Users.AnyAsync(x => x.Id == userId && x.IsProfileSetup);

        if (!isProfileSetup)
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
            return;
        }
        await next(context);
    }
}

public static class ProfileSetupRedirectionMiddlewareImplementation
{
    public static IServiceCollection AddProfileSetupRedirection(this IServiceCollection services)
    {
        return services.AddTransient<ProfileSetupRedirectionMiddleware>();
    }

    public static IApplicationBuilder UseProfileSetupRedirection(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ProfileSetupRedirectionMiddleware>();
    }
}