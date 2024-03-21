using DrugStore.Persistence;
using DrugStore.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DrugStore.IntegrationTest.Fixtures;

public abstract class BaseEfRepoTestFixture
{
    protected ApplicationDbContext DbContext;

    protected BaseEfRepoTestFixture()
    {
        var options = CreateNewContextOptions();
        DbContext = new(options);
    }

    private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        builder.UseInMemoryDatabase(nameof(DrugStore).ToLowerInvariant())
            .AddInterceptors(new AuditableEntityInterceptor())
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
}