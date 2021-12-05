using CaffStore.IdentityProvider.Data;
using CaffStore.IdentityProvider.Models;
using CaffStore.IdentityProvider.Services;  
using Microsoft.VisualStudio.TestTools.UnitTesting;

    [Testclass]
    public class UnitTestIdentity
    {
        [TestMethod]
        public void UserRegistrationWithCorrectEmail()
        {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var user = new ApplicationUser("admin@admin.hu");
        string adminPassword = "Asdf1234.";
        var boolean = false;
        var createUser = await userManager.CreateAsync(user, adminPassword);
        if (createUser.Succeeded)
        {
            boolean = true;
        
        }
        Assert.IsTrue(boolean,"User needs to be created.");
        }


        [TestMethod]
        public void UserRegistrationWithBadEmail()
        {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var user = new ApplicationUser("badform");
        string adminPassword = "Asdf12345.";
        var boolean = true;

        var createUser = await userManager.CreateAsync(user, adminPassword);
        if (createUser.Succeeded)
        {
            boolean = false;
        
        }
        Assert.IsTrue(boolean,"User should not be created.");
        }



        [TestMethod]
        public void UserLoginWithExistingAccount()
        {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var user = new ApplicationUser("admin@amin.hu");
        string adminPassword = "Asdf1234.";
        var boolean = false;

        var createUser = await userManager.FindByEmailAsync(user);
        if (createUser.Succeeded)
        {
            boolean = true;
        
        }
        Assert.IsTrue(boolean,"User should be logged in.");
        }


        [TestMethod]
        public void UserLoginWithBadAccount()
        {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        var user = new ApplicationUser("admin");
        string adminPassword = "Asdf1234.";
        var boolean = true;

        var createUser = await userManager.FindByEmailAsync(user);
        if (createUser.Succeeded)
        {
            boolean = false;
        
        }
        Assert.IsTrue(boolean,"User should not be logged in.");
        }

}
        