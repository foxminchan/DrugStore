using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using Polly;
using Polly.Retry;

namespace DrugStore.Infrastructure.Storage.Minio.Internal;

public sealed class MinioService(IMinioClient minioClient) : IMinioService
{
    private readonly AsyncRetryPolicy _retryPolicy = Policy.Handle<System.Exception>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async Task<string> UploadFileAsync(IFormFile file, string bucketName)
    {
        var objectName = Guid.NewGuid().ToString();

        await EnsureBucketExistsAsync(bucketName);

        await _retryPolicy.ExecuteAsync(async () =>
        {
            await using var stream = file.OpenReadStream();
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length);

            await minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
        });

        var statObjectArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        var statObject = await minioClient.StatObjectAsync(statObjectArgs);

        return statObject.ObjectName;
    }

    public async Task RemoveFileAsync(string bucketName, string objectName)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        await _retryPolicy.ExecuteAsync(async () => await minioClient.RemoveObjectAsync(removeObjectArgs));
    }

    private async Task EnsureBucketExistsAsync(string bucketName)
    {
        if (!await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName)).ConfigureAwait(false))
            await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName)).ConfigureAwait(false);
    }
}