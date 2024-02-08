using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shoporium.Entities.Options;

namespace Shoporium.Business.Services
{
    public class AzureService : IAzureService
    {
        private readonly AzureOptions _azureOptions;
        private readonly ILogger<AzureService> _logger;

        public AzureService(AzureOptions azureOptions, ILogger<AzureService> logger)
        {
            _azureOptions = azureOptions;
            _logger = logger;
        }

        public async Task UploadBlobAsync(string containerName, string blobName, IFormFile file)
        {
            try
            {
                var container = new BlobContainerClient(_azureOptions.StorageConnectionString, containerName);
                var blobClient = container.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                    throw new InvalidOperationException($"Blob {blobName} already exists. Container: {containerName}.");

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during blob upload.");
                throw;
            }
        }

        public async Task<bool> ExistsBlob(string containerName, string blobName)
        {
            var container = new BlobContainerClient(_azureOptions.StorageConnectionString, containerName);
            var blobClient = container.GetBlobClient(blobName);

            return await blobClient.ExistsAsync();
        }

        public async Task<byte[]> DownloadBlobAsync(string containerName, string blobName)
        {
            try
            {
                var container = new BlobContainerClient(_azureOptions.StorageConnectionString, containerName);
                var blobClient = container.GetBlobClient(blobName);

                if (!(await blobClient.ExistsAsync()))
                    throw new InvalidOperationException($"Blob {blobName} doesn't exist. Container: {containerName}.");

                await using var stream = new MemoryStream();
                await blobClient.DownloadToAsync(stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during blob download.");
                throw;
            }
        }

        public async Task DeleteFile(string containerName, string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    return;

                var container = new BlobContainerClient(_azureOptions.StorageConnectionString, containerName);
                var blobs = container.GetBlobsByHierarchy(prefix: path);
                if (!blobs.Any()) return;

                foreach (var blob in blobs)
                {
                    if (blob.IsBlob)
                    {
                        await container.DeleteBlobAsync(blob.Blob.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = $"Error from DeleteFileOnCloud method for container : {containerName}, path : {path}";
                _logger.LogError(ex, msg);
                throw new InvalidOperationException(msg);
            }
        }

        public bool ExistsFile(string containerName, string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var container = new BlobContainerClient(_azureOptions.StorageConnectionString, containerName);
            var blobs = container.GetBlobsByHierarchy(prefix: path);
            return blobs.Any();
        }
    }
}
