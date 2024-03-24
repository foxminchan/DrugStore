using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace DrugStore.Infrastructure.Storage.Local.Internal;

public sealed class LocalStorage(ILogger<LocalStorage> logger) : ILocalStorage
{
    private readonly AsyncRetryPolicy _retryPolicy = Policy.Handle<System.Exception>()
        .WaitAndRetryAsync(
            3,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            (_, retryCount, _) =>
                logger.LogWarning("[{Service}] Retry with attempt {RetryCount}", nameof(LocalStorage), retryCount)
        );

    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var newFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Pics", newFileName);

        logger.LogInformation("[{Service}] Uploading file {FileName} to {FilePath}", nameof(LocalStorage), newFileName,
            filePath);

        await _retryPolicy.ExecuteAsync(async () =>
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);
        });

        return newFileName;
    }

    public async Task RemoveFileAsync(string id, CancellationToken cancellationToken = default) =>
        await _retryPolicy.ExecuteAsync(async () =>
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Pics", id);
            await Task.Run(() =>
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }, cancellationToken);
        });
}