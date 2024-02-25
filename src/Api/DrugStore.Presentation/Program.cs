using Asp.Versioning.Builder;
using DrugStore.Domain.Identity;
using DrugStore.Presentation;
using DrugStore.Presentation.Extensions;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity();
builder.Services.AddRateLimiting();
builder.Services.AddCustomCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationService();
builder.Services.AddEndpoints(AssemblyReference.Program);
builder.Services.AddInfrastructureService(builder);
builder.Services.AddCustomDbContext(builder.Configuration);

Log.Logger.Information("Total services: {count}", builder.Services.Count);

var app = builder.Build();

app.UseCustomCors();
app.UseRateLimiter();
app.UseInfrastructureService();

ApiVersionSet apiVersionSet = app
    .NewApiVersionSet()
    .HasApiVersion(new(1, 0))
    .HasApiVersion(new(2, 0))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionGroupBuilder = app
    .MapGroup("/api/v{apiVersion:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionGroupBuilder);
app.UseAuthentication()
    .UseAuthorization();

versionGroupBuilder
    .MapGroup("/auth")
    .MapIdentityApi<ApplicationUser>()
    .WithTags("Auth")
    .MapToApiVersion(new(1, 0));

app.Map("/", () => Results.Redirect("/swagger"));
app.Map("/error",
        () => Results.Problem("An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError))
    .ExcludeFromDescription();

app.MapPrometheusScrapingEndpoint();

app.Run();
