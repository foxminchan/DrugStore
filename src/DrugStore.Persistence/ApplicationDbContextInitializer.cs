using System.Security.Claims;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Infrastructure.Exception;
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
            logger.LogError(ex, "[{Prefix}] Migration error with message: {Message}",
                nameof(ApplicationDbContextInitializer), ex.Message);
            throw new DatabaseInitializationException("An error occurred while initializing the database", ex);
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
            logger.LogError(ex, "[{Prefix}] Seeding error with message: {Message}",
                nameof(ApplicationDbContextInitializer), ex.Message);
            throw new DatabaseInitializationException("An error occurred while initializing the database", ex);
        }
    }

    private async Task TrySeedAsync()
    {
        ApplicationRole admin = new(Roles.ADMIN);

        if (!await roleManager.RoleExistsAsync(Roles.ADMIN))
        {
            await roleManager.CreateAsync(admin);
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.MANAGE));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.READ));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, Claims.WRITE));
        }
        else
        {
            logger.LogDebug("[{Prefix}] admin already exists", nameof(ApplicationDbContextInitializer));
        }

        ApplicationRole customer = new(Roles.CUSTOMER);

        if (!await roleManager.RoleExistsAsync(Roles.CUSTOMER))
        {
            await roleManager.CreateAsync(customer);
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.READ));
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, Claims.WRITE));
        }
        else
        {
            logger.LogDebug("[{Prefix}] customer already exists", nameof(ApplicationDbContextInitializer));
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
                new(JwtClaimTypes.PhoneNumber, administrator.PhoneNumber!)
            ]).Result;

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.First().Description);

            await userManager.AddToRoleAsync(administrator, admin.Name!);

            logger.LogDebug("[{Prefix}] administrator created", nameof(ApplicationDbContextInitializer));
        }
        else
        {
            logger.LogDebug("[{Prefix}] administrator already exists", nameof(ApplicationDbContextInitializer));
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
                new(JwtClaimTypes.PhoneNumber, user.PhoneNumber!)
            ]).Result;

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.First().Description);

            await userManager.AddToRoleAsync(user, customer.Name!);

            logger.LogDebug("[{Prefix}] user created", nameof(ApplicationDbContextInitializer));
        }
        else
        {
            logger.LogDebug("[{Prefix}] user already exists", nameof(ApplicationDbContextInitializer));
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