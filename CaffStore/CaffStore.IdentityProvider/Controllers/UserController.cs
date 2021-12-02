using CaffStore.IdentityProvider.Data;
using CaffStore.IdentityProvider.Dtos;
using CaffStore.IdentityProvider.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaffStore.IdentityProvider.Controllers;

[Authorize]
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
        return users.Select(x => new UserDto
        {
            Id = x.Id,
            UserName = x.UserName
        });
    }

    [HttpPost]
    public async Task SetUsername(string userId, string username)
    {
        var user = await userManager.FindByIdAsync(userId);
        await userManager.SetUserNameAsync(user, username);
    }
}
