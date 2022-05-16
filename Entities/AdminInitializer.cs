using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
namespace Entities
{
    public class AdminInitializer
    {
        
        private static string UserName => "Admin";
        private static string Password => "124578369Aa";

        public static async Task InitializeAsync(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole()
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            };

            var admin = new User() 
            { 
                UserName = "Admin",
                Email ="Admin@gmail.com"
            };

            if (await roleManager.FindByNameAsync(adminRole.Name) == null)
            {
                await roleManager.CreateAsync(adminRole);
            }

            if (await userManager.FindByNameAsync(UserName) == null)
            { 
                var result = await userManager.CreateAsync(admin, Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, adminRole.Name);
                }
            }
        }
    }
}
