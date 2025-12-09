using Microsoft.AspNetCore.Identity;
using TalentoPlus.API.Data.Entities;

namespace TalentoPlus.API.Data.Seeders
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<Employee>>();

            // Create roles
            string[] roles = { "Admin", "Employee"};

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create default admin account
            string adminEmail = "admin@talentoplus.com";
            string adminPassword = "Admin123$";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new Employee
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    
                    Position = "Administrator",
                    Salary = 0,              
                    HireDate = DateTime.UtcNow,
                    IsActive = true,

                    EducationLevel = "N/A",
                    ProfessionalProfile = "System Administrator",
                    Department = "Administration",
                };

                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
