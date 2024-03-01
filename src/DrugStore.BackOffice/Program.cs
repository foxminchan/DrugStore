using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.AddOpenTelemetry(builder.Configuration);
builder.AddSerilog(builder.Environment.ApplicationName);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapPrometheusScrapingEndpoint();
app.Run();