using Microsoft.AspNetCore.Http;

namespace DrugStore.Infrastructure.Storage.Local;

public interface ILocalStorage
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
    Task RemoveFileAsync(string path, CancellationToken cancellationToken = default);
}