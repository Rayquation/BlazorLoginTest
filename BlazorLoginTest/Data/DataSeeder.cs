using BlazorLoginTest.Data;
using Microsoft.AspNetCore.Identity;

public class DataSeeder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserManager<ApplicationUser> _userManager;

    public DataSeeder(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
    {
        _serviceProvider = serviceProvider;
        _userManager = userManager;
    }

    public async Task SeedRolesAndAdminUserAsync()
    {
        var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Create Admin role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Create Admin user if it doesn't exist
        var adminUser = await _userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(adminUser, "Admin@123");

            // Assign Admin role to the user
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
