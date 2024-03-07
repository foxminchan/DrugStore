using Microsoft.Extensions.Diagnostics.HealthChecks;
using Minio;
using Minio.DataModel.Args;

namespace DrugStore.Infrastructure.HealthCheck;

public sealed class MinioHealthCheck(IMinioClient minioClient) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(nameof(MinioHealthCheck).ToLowerInvariant());
            var bucketExists = await minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
            return !bucketExists ? HealthCheckResult.Unhealthy() : HealthCheckResult.Healthy();
        }
        catch (System.Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}