namespace Pizzas.Core.Abstractions.Services.Main;

public interface IAzureBlobStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
}