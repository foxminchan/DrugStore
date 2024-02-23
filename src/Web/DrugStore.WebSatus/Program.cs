using DrugStore.Infrastructure.HealthCheck;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddHealthCheck();
builder.Logging.AddJsonConsole();

var app = builder.Build();

app.MapHealthCheck();

app.Map("/", () => Results.Redirect("/hc-ui"));

app.Run();
