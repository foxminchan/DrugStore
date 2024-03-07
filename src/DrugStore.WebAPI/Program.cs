using DrugStore.Persistence;
using DrugStore.WebAPI.Extensions;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.AddIdentity();
builder.AddCustomDbContext();
builder.Services.AddEndpoints();
builder.Services.AddCustomCors();
builder.Services.AddAntiforgery();
builder.Services.AddRateLimiting();
builder.Services.AddApplicationService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureService(builder);

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

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

app.UseAntiforgery();
app.UseAuthentication()
    .UseAuthorization();

app.Map("/", () => Results.Redirect("/swagger"));
app.Map("/error",
        () => Results.Problem("An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError))
    .ExcludeFromDescription();

app.MapPrometheusScrapingEndpoint();

app.Run();