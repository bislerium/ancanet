using ancanet.server.Data;
using ancanet.server.Dtos.Account;
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
    public class AccountController(AppDbContext dbContext): ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Setup(ConfigureProfileRequestDto requestDto)
        {
            var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id is null) return BadRequest();

            var user = await dbContext.Users.FindAsync(id);
            if (user is null)  return NotFound();

            user.IsProfileSetup = true;

            dbContext.UserProfiles.Add(new()
            {
                AppUserId = id,
                FullName = requestDto.FullName,
                Gender = requestDto.Gender,
                DateOfBirth = requestDto.DateOfBirth,
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
