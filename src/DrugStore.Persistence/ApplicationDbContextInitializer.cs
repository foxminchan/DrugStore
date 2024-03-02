using System.Security.Claims;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
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
        ApplicationRole admin = new(Roles.Admin);

        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            await roleManager.CreateAsync(admin);
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.Manage));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.Read));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.Write));
        }

        ApplicationRole customer = new(Roles.Customer);
        if (!await roleManager.RoleExistsAsync(Roles.Customer))
        {
            await roleManager.CreateAsync(customer);
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.Read));
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.Write));
        }

        const string password = "P@ssw0rd";

        ApplicationUser administrator = new()
        {
            UserName = "nguyenxuannhan@gmail.com",
            Email = "nguyenxuannhan@gmail.com",
            FullName = "Nguyen Xuan Nhan",
            PhoneNumber = "0123456789",
            Address = new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh")
        };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, password);
            await userManager.AddToRoleAsync(administrator, admin.Name!);
        }

        ApplicationUser user = new()
        {
            UserName = "lelavy@gmail.com",
            Email = "lelavy@gmail.com",
            FullName = "Le La Vy",
            PhoneNumber = "0123456789",
            Address = new("Xa Lo Ha Noi", "Thu Duc", "Ho Chi Minh")
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