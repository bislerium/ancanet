using System.Security.Claims;
using ancanet.server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ancanet.server.Identity.Ucpf;

public class AdditionalUserClaimsPrincipalFactory(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<AppUser, IdentityRole>(userManager, roleManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
    {
        var principal = await base.CreateAsync(user);
        var identity = (ClaimsIdentity)principal.Identity!;

        var claims = new List<Claim>
        {
            user.TwoFactorEnabled 
                ? new Claim("amr", "mfa") 
                : new Claim("amr", "pwd")
        };
        
        identity.AddClaims(claims);
        return principal;
    }
}

public static class Injection
{
    public static IServiceCollection AddAdditionalUserClaimsPrincipalFactory(this IServiceCollection services)
    {
        return services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, 
            AdditionalUserClaimsPrincipalFactory>();
    }
}
