using Ardalis.Result;
using DrugStore.Infrastructure.Storage.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Infrastructure.Storage.Cloudinary;

public interface ICloudinaryService
{
    Task<Result<CloudinaryResult>> AddPhotoAsync(IFormFile? file);
    Task<Result<string>> DeletePhotoAsync(string publicId);
}
