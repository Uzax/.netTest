using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Mango.Services.AuthAPI.Extensions
{
    public class SeedRoles
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUsers>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "AppDbContext is not registered in the service provider.");
            }

            // Seed roles
            string[] roles = new string[] { "Admin", "User" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(role));

                    if (!roleResult.Succeeded)
                    {
                        throw new Exception($"Failed to create role {role}: " +
                            string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }
                }
            }

            // Ensure that changes are saved before assigning roles to users
            await context.SaveChangesAsync();
            //
            // // Find or create the user "uzax"
            var user = await userManager.FindByNameAsync("zayed");
            //
            // if (user == null)
            // {
            //     user = new ApplicationUsers
            //     {
            //         UserName = "uzax",
            //         Email = "uzax@example.com",
            //         EmailConfirmed = true // Set this to false if email confirmation is needed
            //     };
            //
            //     // Create the user with a password
            //     var createUserResult = await userManager.CreateAsync(user, "DefaultPassword123!");
            //
            //     if (!createUserResult.Succeeded)
            //     {
            //         throw new Exception("Failed to create user uzax: " +
            //             string.Join(", ", createUserResult.Errors.Select(e => e.Description)));
            //     }
            // }
            //
            // // Ensure "Admin" role is assigned to "uzax"
            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                var addToRoleResult = await userManager.AddToRoleAsync(user, "Admin");
            
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception("Failed to assign Admin role to uzax: " +
                        string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
