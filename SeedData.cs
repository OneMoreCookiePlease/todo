using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Todo
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);

        
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager) {
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager) {

            var testAdmin = await userManager.Users.Where( u => u.UserName == "olexii.brncal@gmail.com").SingleOrDefaultAsync();

            //if (testAdmin != null) return;

            //testAdmin = new IdentityUser
            //{
            //    UserName = "admin2@todo.local",
            //    Email = "admi21@todo.local"
            //};
            //await userManager.CreateAsync(testAdmin, "NotSecure123!!");
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
    }
}