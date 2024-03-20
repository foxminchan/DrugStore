using System.Security.Claims;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using FluentValidation;
using IdentityModel;
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
        else
        {
            logger.LogDebug("admin already exists");
        }

        ApplicationRole customer = new(Roles.Customer);

        if (!await roleManager.RoleExistsAsync(Roles.Customer))
        {
            await roleManager.CreateAsync(customer);
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.Read));
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.Write));
        }
        else
        {
            logger.LogDebug("customer already exists");
        }

        const string password = "P@ssw0rd";

        ApplicationUser administrator = new(
            "nguyenxuannhan@gmail.com",
            "Nguyen Xuan Nhan",
            "0123456789",
            new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh")
        );

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            var result = userManager.CreateAsync(administrator, password).Result;

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.First().Description);

            result = userManager.AddClaimsAsync(administrator,
            [
                new(JwtClaimTypes.Name, administrator.FullName!),
                new(JwtClaimTypes.Email, administrator.Email!),
                new(JwtClaimTypes.PhoneNumber, administrator.PhoneNumber!),
            ]).Result;

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.First().Description);

            await userManager.AddToRoleAsync(administrator, admin.Name!);

            logger.LogDebug("administrator created");
        }
        else
        {
            logger.LogDebug("administrator already exists");
        }

        ApplicationUser user = new(
            "lelavy@gmail.com",
            "Le La Vy",
            "0123456789",
            new("Xa Lo Ha Noi", "Thu Duc", "Ho Chi Minh")
        );

        if (userManager.Users.All(u => u.UserName != user.UserName))
        {
            var result = userManager.CreateAsync(user, password).Result;

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.First().Description);

            result = userManager.AddClaimsAsync(user,
            [
                new(JwtClaimTypes.Name, user.FullName!),
                new(JwtClaimTypes.Email, user.Email!),
                new(JwtClaimTypes.PhoneNumber, user.PhoneNumber!),
            ]).Result;

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.First().Description);

            await userManager.AddToRoleAsync(user, customer.Name!);

            logger.LogDebug("user created");
        }
        else
        {
            logger.LogDebug("user already exists");
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