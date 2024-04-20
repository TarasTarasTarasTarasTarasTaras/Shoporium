using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Shoporium.Business.Services
{
    public class AzureService : IAzureService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;

        public AzureService(IConfiguration configuration)
        {
            _configuration = configuration;

            var awsAccessKeyId = _configuration["AWS:AccessKey"];
            var awsSecretAccessKey = _configuration["AWS:SecretKey"];

            var credentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            _s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.EUCentral1);
        }

        public async Task UploadBlobAsync(string containerName, string blobName, IFormFile file)
        {
            var path = $"{blobName}";

            using (var stream = file.OpenReadStream())
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = path,
                    BucketName = _configuration["AWSBucketName"],
                };

                var fileTransferUtility = new TransferUtility(_s3Client);

                await fileTransferUtility.UploadAsync(uploadRequest);
            }
        }

        public async Task<byte[]> DownloadBlobAsync(string containerName, string blobName)
        {
            var path = $"{blobName}";

            var request = new GetObjectRequest
            {
                BucketName = _configuration["AWSBucketName"],
                Key = path
            };

            using (var response = await _s3Client.GetObjectAsync(request))
            {
                var res = ConvertStreamToByteArray(response.ResponseStream);
                return res;
            }
        }

        private byte[] ConvertStreamToByteArray(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
