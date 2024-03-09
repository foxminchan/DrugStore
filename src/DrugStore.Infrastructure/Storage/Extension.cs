using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Infrastructure.Storage.Local.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DrugStore.Infrastructure.Storage;

public static class Extension
{
    public static IServiceCollection AddLocalStorage(this IHostApplicationBuilder builder)
        => builder.Services.AddScoped<ILocalStorage, LocalStorage>();
}