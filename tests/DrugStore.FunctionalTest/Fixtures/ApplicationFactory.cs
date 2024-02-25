using DotNet.Testcontainers.Containers;
using DrugStore.FunctionalTest.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace DrugStore.FunctionalTest.Fixtures;

public sealed class ApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
{
    private readonly List<IContainer> _containers = [];
    public WebApplicationFactory<TProgram> Instance { get; private set; } = default!;

    public Task InitializeAsync()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Test";
        Instance = WithWebHostBuilder(builder => builder.UseEnvironment(env));
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
        => Task
            .WhenAll(_containers.Select(container => container.DisposeAsync().AsTask()))
            .ContinueWith(async _ => await base.DisposeAsync());

    public ApplicationFactory<TProgram> WithCacheContainer()
    {
        _containers.Add(new RedisBuilder()
            .WithName($"test_cache_{Guid.NewGuid()}")
            .WithImage("redis:alpine")
            .WithCleanUp(true)
            .Build());

        return this;
    }

    public ApplicationFactory<TProgram> WithDbContainer()
    {
        _containers.Add(new PostgreSqlBuilder()
            .WithDatabase($"test_db_{Guid.NewGuid()}")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithImage("postgres:alpine")
            .WithCleanUp(true)
            .Build());

        return this;
    }

    public async Task StartContainersAsync(CancellationToken cancellationToken = default)
    {
        if (_containers.Count == 0)
            return;

        await Task.WhenAll(_containers.Select(container =>
            container.StartWithWaitAndRetryAsync(cancellationToken: cancellationToken)));

        Instance = _containers.Aggregate(this as WebApplicationFactory<TProgram>, (current, container) => current.WithWebHostBuilder(builder =>
        {
            switch (container)
            {
                case PostgreSqlContainer dbContainer:
                    builder.UseSetting("ConnectionStrings:Postgres", dbContainer.GetConnectionString());
                    break;

                case RedisContainer cacheContainer:
                    builder.UseSetting("ConnectionStrings:Redis", cacheContainer.GetConnectionString());
                    break;
            }
        }));
    }

    public new HttpClient CreateClient() => Instance.CreateClient();

    public async Task StopContainersAsync()
    {
        if (_containers.Count == 0)
            return;

        await Task.WhenAll(_containers.Select(container => container.DisposeAsync().AsTask()))
            .ContinueWith(async _ => await base.DisposeAsync())
            .ContinueWith(async _ => await InitializeAsync())
            .ConfigureAwait(false);

        _containers.Clear();
    }
}
