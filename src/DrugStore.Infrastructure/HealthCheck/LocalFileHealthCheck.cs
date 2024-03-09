using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DrugStore.Infrastructure.HealthCheck;

public sealed class LocalFileHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "Pics");
        return Task.FromResult(!Directory.Exists(directory) 
            ? HealthCheckResult.Unhealthy("Pics directory does not exist") 
            : HealthCheckResult.Healthy());
    }
}