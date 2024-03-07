using Microsoft.AspNetCore.Http;

namespace DrugStore.Infrastructure.Storage.Minio;

public interface IMinioService
{
    Task<string> UploadFileAsync(IFormFile file, string bucketName);
    Task RemoveFileAsync(string bucketName, string objectName);
}