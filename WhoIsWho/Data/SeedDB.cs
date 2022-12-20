using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WhoIsWho.Models.Entities;

namespace WhoIsWho.Data
{
    public static class SeedDB
    {
        public static async Task Initialize(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            dbContext.Database.Migrate();

            var adminRole = await roleManager.FindByNameAsync("admin");

            if(adminRole == null)
            {
                adminRole = new IdentityRole<int> { Name = "admin" };
                await roleManager.CreateAsync(adminRole);
            }

            var admin = await userManager.FindByEmailAsync("admin@mail.ru");
            if(admin == null)
            {
                admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
                await userManager.CreateAsync(admin, "Qwerty_123");
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
