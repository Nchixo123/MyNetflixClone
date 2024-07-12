using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using MyNetflixClone.Interfaces;

namespace MyNetflixClone.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _configuration = configuration;
        }

        public async Task<string> UploadMovieAsync(Stream fileStream, string fileName)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);

            var bucketName = _configuration["AWS:BucketName"];
            var region = _configuration["AWS:Region"];
            await fileTransferUtility.UploadAsync(fileStream, bucketName, fileName);

            return $"https://{bucketName}.s3.{region}.amazonaws.com/{fileName}";
        }

        public string GenerateSecureUrl(string key)
        {
            var bucketName = _configuration["AWS:BucketName"];
            var region = _configuration["AWS:Region"];

            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddHours(1), // URL valid for 1 hour
                Verb = HttpVerb.GET
            };

            return _s3Client.GetPreSignedURL(request);
        }
    }
}
