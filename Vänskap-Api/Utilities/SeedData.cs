using Microsoft.AspNetCore.Identity;
using Vänskap_Api.Models;

namespace Vänskap_Api.Utilities
{
    public class SeedData
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin123!";

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = adminEmail,
                    LastName = adminEmail,
                    DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow)
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        public static async Task SeedTestUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var testUsers = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Erik", LastName = "Andersson", UserName = "erik.andersson", Email = "erik.andersson@test.com", DateOfBirth = new DateOnly(1990, 1, 15) },
                new ApplicationUser { FirstName = "Anna", LastName = "Johansson", UserName = "anna.johansson", Email = "anna.johansson@test.com", DateOfBirth = new DateOnly(1992, 3, 22) },
                new ApplicationUser { FirstName = "Johan", LastName = "Karlsson", UserName = "johan.karlsson", Email = "johan.karlsson@test.com", DateOfBirth = new DateOnly(1988, 6, 5) },
                new ApplicationUser { FirstName = "Maria", LastName = "Svensson", UserName = "maria.svensson", Email = "maria.svensson@test.com", DateOfBirth = new DateOnly(1995, 9, 12) },
                new ApplicationUser { FirstName = "Lars", LastName = "Nilsson", UserName = "lars.nilsson", Email = "lars.nilsson@test.com", DateOfBirth = new DateOnly(1991, 11, 3) },
                new ApplicationUser { FirstName = "Karin", LastName = "Eriksson", UserName = "karin.eriksson", Email = "karin.eriksson@test.com", DateOfBirth = new DateOnly(1993, 2, 28) },
                new ApplicationUser { FirstName = "Per", LastName = "Larsson", UserName = "per.larsson", Email = "per.larsson@test.com", DateOfBirth = new DateOnly(1989, 7, 19) },
                new ApplicationUser { FirstName = "Sofia", LastName = "Olsson", UserName = "sofia.olsson", Email = "sofia.olsson@test.com", DateOfBirth = new DateOnly(1994, 5, 8) },
                new ApplicationUser { FirstName = "Mattias", LastName = "Pettersson", UserName = "mattias.pettersson", Email = "mattias.pettersson@test.com", DateOfBirth = new DateOnly(1990, 12, 1) },
                new ApplicationUser { FirstName = "Elin", LastName = "Berg", UserName = "elin.berg", Email = "elin.berg@test.com", DateOfBirth = new DateOnly(1996, 4, 16) },
                new ApplicationUser { FirstName = "Test", LastName = "Test", UserName = "IAmTest", Email = "iamtest@test.com", DateOfBirth = new DateOnly(2000, 1, 1) }
            };

            foreach (var user in testUsers)
            {
                if (await userManager.FindByNameAsync(user.UserName!) == null)
                {
                    user.EmailConfirmed = true;
                    await userManager.CreateAsync(user, "Test123!");
                }
            }
        }
    }
}
