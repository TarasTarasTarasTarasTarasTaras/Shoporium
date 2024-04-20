using Microsoft.AspNetCore.Http;

namespace Shoporium.Business.Services
{
    public interface IAzureService
    {
        Task<byte[]> DownloadBlobAsync(string containerName, string blobName);
        Task UploadBlobAsync(string containerName, string blobName, IFormFile file);
    }
}