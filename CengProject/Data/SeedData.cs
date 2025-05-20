using Microsoft.AspNetCore.Identity;
using CengProject.Models;

namespace CengProject.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Gerekli roller
            string[] roles = { "Admin", "Instructor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Admin kullanıcı
            const string adminEmail = "admin@example.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    IsActive = true // 🔥 Add this
                };

                var result = await userManager.CreateAsync(adminUser, "AdminPass1!");
               if (result.Succeeded)
{
    await userManager.AddToRoleAsync(adminUser, "Admin");
}
else
{
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"[SeedData Error] {error.Description}");
    }
}

            }
        }
    }
}