﻿using System.Security.Claims;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Helpers;
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
        ApplicationRole admin = new(RoleHelper.Admin);

        logger.LogInformation("Admin role: {Admin}", admin.Name);

        if (!await roleManager.RoleExistsAsync(RoleHelper.Admin))
        {
            await roleManager.CreateAsync(admin);
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, ClaimHelper.Manage));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, ClaimHelper.Read));
            await roleManager.AddClaimAsync(admin, new(ClaimTypes.Role, ClaimHelper.Write));
        }

        ApplicationRole customer = new(RoleHelper.Customer);

        logger.LogInformation("Customer role: {Customer}", customer.Name);

        if (!await roleManager.RoleExistsAsync(RoleHelper.Customer))
        {
            await roleManager.CreateAsync(customer);
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, ClaimHelper.Read));
            await roleManager.AddClaimAsync(customer, new(ClaimTypes.Role, ClaimHelper.Write));
        }

        const string password = "P@ssw0rd";

        ApplicationUser administrator = new(
            "nguyenxuannhan@gmail.com",
            "Nguyen Xuan Nhan",
            "0123456789",
            new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh")
        );

        logger.LogInformation("Admin user: {Admin}", administrator.UserName);

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, password);
            await userManager.AddToRoleAsync(administrator, admin.Name!);
        }

        ApplicationUser user = new(
            "lelavy@gmail.com",
            "Le La Vy",
            "0123456789",
            new("Xa Lo Ha Noi", "Thu Duc", "Ho Chi Minh")
        );

        logger.LogInformation("Customer user: {Customer}", user.UserName);

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