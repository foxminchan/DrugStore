using DrugStore.Domain.IdentityAggregate;
using DrugStore.Persistence;
using DrugStore.Presentation.Extensions;
using AssemblyReference = DrugStore.Presentation.AssemblyReference;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity();
builder.Services.AddRateLimiting();
builder.Services.AddCustomCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationService();
builder.Services.AddEndpoints(AssemblyReference.Program);
builder.Services.AddInfrastructureService(builder);
builder.Services.AddCustomDbContext(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) await app.InitializeDatabaseAsync();

app.UseCustomCors();
app.UseRateLimiter();
app.UseInfrastructureService();

var apiVersionSet = app
    .NewApiVersionSet()
    .HasApiVersion(new(1, 0))
    .HasApiVersion(new(2, 0))
    .ReportApiVersions()
    .Build();

var versionGroupBuilder = app
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