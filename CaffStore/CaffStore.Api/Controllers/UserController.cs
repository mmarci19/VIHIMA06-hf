using CaffStore.IdentityProvider.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaffStore.IdentityProvider.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly ApplicationDbContext context;
        //private readonly UserManager<ApplicationUser> userManager;

        //public UserController(ApplicationDbContext context)
        //{
        //    this.context = context;
        //    //this.userManager = userManager;
        //}

        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            //var users = await context.Users.ToListAsync();
            return new List<UserDto>();
        }

        [HttpPost]
        public async Task SetUsername(string userId, string username)
        {

        }
    }
}


