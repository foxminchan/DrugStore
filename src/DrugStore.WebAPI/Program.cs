using Ardalis.ListStartupServices;
using DrugStore.Persistence;
using DrugStore.WebAPI.Extensions;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddIdentity();
builder.AddMappings();
builder.AddCustomDbContext();
builder.AddEndpoints();
builder.AddCustomCors();
builder.AddRateLimiting();
builder.AddApplicationService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureService(builder);
builder.Services.Configure<ServiceConfig>(config => config.Services = [..builder.Services]);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
    app.UseShowAllServicesMiddleware();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseCustomCors();
app.UseRateLimiter();
app.UseInfrastructureService();

if (!Directory.Exists(Path.Combine(app.Environment.ContentRootPath, "Pics")))
    Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath, "Pics"));

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Pics")),
    RequestPath = "/pics"
});

app.MapIdentity();
app.MapEndpoints();
app.MapSpecialEndpoints();
app.UseHttpsRedirection();

try
{
    app.Logger.LogInformation("Applying database migration ({ApplicationName})...",
        builder.Environment.ApplicationName);
    app.ApplyDatabaseMigration();
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", builder.Environment.ApplicationName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...",
        builder.Environment.ApplicationName);
}
finally
{
    Log.CloseAndFlush();
}