using Microsoft.AspNetCore.Identity;
using OnlineClinic.Models.AuthorizationAuthentication.Context;
using OnlineClinic.Models.AuthorizationAuthentication.Model;
using System.Threading.Tasks;

namespace OnlineClinic.Models.AuthorizationAuthentication.InitializerAcaunt
{
    public partial class DbInitializer
    {
        public static async Task InitializeAsync(AccountDbContext accountContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
                accountContext.SaveChanges();
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
                accountContext.SaveChanges();
            }
            if (await roleManager.FindByNameAsync("doctor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("doctor"));
                accountContext.SaveChanges();
            }
            string adminNik = "Boss";
            string password = "&Aa1234";
            if (await userManager.FindByNameAsync(adminNik) == null)
            {
                User admin = new User
                {
                    UserName = adminNik,
                    AddressCity = "Гомель",
                    Lastname = "Главный"
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                    accountContext.SaveChanges();
                }
            }
        }
    }
}
