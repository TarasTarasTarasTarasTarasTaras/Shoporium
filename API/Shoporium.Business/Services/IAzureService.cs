using Microsoft.AspNetCore.Http;

namespace Shoporium.Business.Services
{
    public interface IAzureService
    {
        Task DeleteFile(string containerName, string path);
        Task<byte[]> DownloadBlobAsync(string containerName, string blobName);
        Task<bool> ExistsBlob(string containerName, string blobName);
        bool ExistsFile(string containerName, string path);
        Task UploadBlobAsync(string containerName, string blobName, IFormFile file);
    }
}