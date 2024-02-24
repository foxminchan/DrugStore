using DrugStore.Presentation.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomDbContext(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
