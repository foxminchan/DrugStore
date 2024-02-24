using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DrugStore.Infrastructure.Kestrel;

public static class Extension
{
    public static void AddKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
            options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
            options.Limits.MinRequestBodyDataRate = new(100, TimeSpan.FromSeconds(10));
            options.Limits.MinResponseDataRate = new(100, TimeSpan.FromSeconds(10));
            options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
            options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
        });

        builder.Services.AddResponseCompression()
            .AddResponseCaching(options => options.MaximumBodySize = 1024)
            .AddRouting(options => options.LowercaseUrls = true);
    }

    public static void UseKestrel(this IApplicationBuilder app)
    {
        app.UseHsts();
        app.UseResponseCompression();
        app.UseResponseCaching();
        app.UseHttpsRedirection();
    }
}
