using System.Security.Claims;
using DrugStore.Domain.Identity.Constants;
using DrugStore.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DrugStore.Persistence;

public sealed class ApplicationDbContextInitializer(
    ApplicationDbContext context,
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager,
    ILogger<ApplicationDbContextInitializer> logger)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migration error");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Seeding error");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        var admin = new ApplicationRole(Roles.Admin);

        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(admin);
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.Manage));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.Read));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.Write));   
        }

        var customer = new ApplicationRole(Roles.Customer);
        if (!await roleManager.RoleExistsAsync(Roles.Customer))
        {
            await roleManager.CreateAsync(customer);
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.Read));
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.Write));
        }

        const string password = "P@ssw0rd";

        var administrator = new ApplicationUser
        {
            UserName = "nguyenxuannhan@gmail.com",
            Email = "nguyenxuannhan@gmail.com",
            FullName = "Nguyen Xuan Nhan",
            Phone = "0123456789"
        };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, password);
            await userManager.AddToRoleAsync(administrator, admin.Name!);
        }

        var user = new ApplicationUser
        {
            UserName = "lelavy@gmail.com",
            Email = "lelavy@gmail.com",
            FullName = "Le La Vy",
            Phone = "0123456789"
        };

        if (userManager.Users.All(u => u.UserName != user.UserName))
        {
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, customer.Name!);
        }
    }
}

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
    }
}
