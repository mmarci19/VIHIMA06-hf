using CaffStore.IdentityProvider.Data;
using CaffStore.IdentityProvider.Dtos;
using CaffStore.IdentityProvider.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaffStore.IdentityProvider.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<ApplicationUser> userManager;

    public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get()
    {
        var users = await context.Users.ToListAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            UserName = user.UserName
        });
    }

    [HttpPost]
    public async Task SetUsername(string userId, string username)
    {
        var user = await userManager.FindByIdAsync(userId);
        await userManager.SetUserNameAsync(user, username);
    }
}
