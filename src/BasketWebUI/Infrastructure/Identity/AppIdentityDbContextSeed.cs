using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BasketWebUI.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "demouser@panch.com", Email = "demouser@panch.com" };
            await userManager.CreateAsync(defaultUser, "Pass@word1");
        }
    }
}
