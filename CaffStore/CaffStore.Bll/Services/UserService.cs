using CaffStore.Bll.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CaffStore.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetCurrentUserId()
        {
            return httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetCurrentUserName()
        {
            return httpContext.HttpContext.User.FindFirstValue("username");
        }

        public string GetCurrentUserRole()
        {
            return httpContext.HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
