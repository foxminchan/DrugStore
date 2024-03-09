using Microsoft.AspNetCore.Http;
using Polly;
using Polly.Retry;

namespace DrugStore.Infrastructure.Storage.Local.Internal;

public sealed class LocalStorage : ILocalStorage
{
    private readonly AsyncRetryPolicy _retryPolicy = Policy.Handle<System.Exception>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var newFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Pics", newFileName);

        await _retryPolicy.ExecuteAsync(async () =>
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);
        });

        return newFileName;
    }

    public async Task RemoveFileAsync(string path, CancellationToken cancellationToken = default) =>
        await _retryPolicy.ExecuteAsync(async () =>
        {
            await Task.Run(() =>
            {
                if (File.Exists(path)) File.Delete(path);
            }, cancellationToken);
        });
}