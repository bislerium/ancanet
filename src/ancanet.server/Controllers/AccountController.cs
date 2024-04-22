using ancanet.server.Data;
using ancanet.server.Dtos.Account;
using ancanet.server.Extensions;
using ancanet.server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ancanet.server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController(AppDbContext dbContext, UserManager<AppUser> userManager): ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Setup(ConfigureProfileRequestDto requestDto)
        {
            if (!HttpContext.TryGetUserId(out var userId)) return BadRequest();

            #region User setup

            var user = await userManager.FindByIdAsync(userId);
            if (user is null)  return NotFound();
                        
            user.UserName = requestDto.UserName;
            user.IsProfileSetup = true;

            await userManager.UpdateAsync(user);

            #endregion

            #region Profile setup

            dbContext.UserProfiles.Add(new()
            {
                AppUserId = userId,
                FullName = requestDto.FullName,
                Gender = requestDto.Gender,
                DateOfBirth = requestDto.DateOfBirth,
            });

            await dbContext.SaveChangesAsync();

            #endregion

            return Ok();
        }
    }
}
