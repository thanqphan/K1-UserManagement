using Microsoft.AspNetCore.Identity;
using UserManagement.Core.Entities;

namespace UserManagement.Api
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Staff", "Customer" };
            IdentityResult roleResult;

            // Create roles if they do not exist
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create an admin user
            var adminUser = new User
            {
                UserName = "thangpa",
                Email = "admin@thangpa.com",
                Fullname = "Thang Phan"
            };

            string userPassword = "Password@123";
            var admin = await userManager.FindByEmailAsync("admin@thangpa.com");

            if (admin == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var staffUser = new User
            {
                UserName = "Staff",
                Email = "admin@staff.com",
                Fullname = "Staff Phan"
            };

            var staff = await userManager.FindByEmailAsync("admin@staff.com");

            if (staff == null)
            {
                var createPowerUser = await userManager.CreateAsync(staffUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(staffUser, "Staff");
                }
            }

            var customerUser = new User
            {
                UserName = "Customer",
                Email = "admin@Customer.com",
                Fullname = "Customer Phan"
            };

            var user = await userManager.FindByEmailAsync("admin@Customer.com");

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(customerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, "Customer");
                }
            }
        }
    }

}
