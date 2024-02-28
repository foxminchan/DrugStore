using DotNet.Testcontainers.Containers;

using Polly;

namespace DrugStore.FunctionalTest.Extensions;

public static class TestContainersExtension
{
    public static Task StartWithWaitAndRetryAsync(
        this IContainer container,
        int retryCount = 3,
        int retryDelay = 3,
        CancellationToken cancellationToken = default)
        => Policy
            .Handle<AggregateException>()
            .Or<InvalidOperationException>()
            .WaitAndRetryAsync(retryCount, _ => TimeSpan.FromSeconds(retryCount * retryDelay))
            .ExecuteAsync(container.StartAsync, cancellationToken);
}
