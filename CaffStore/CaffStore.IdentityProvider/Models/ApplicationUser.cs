using Microsoft.AspNetCore.Identity;

namespace CaffStore.IdentityProvider.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() { }
    public ApplicationUser(string email) : base(email) { }
}
