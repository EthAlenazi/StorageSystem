using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Strategy.Implementation
{
    public class S3Storage : IStorage
    {
        private readonly AmazonS3Settings _settings;

        public S3Storage(AmazonS3Settings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task UploadFileAsync(string fileName, Guid fileId)
        {
            Console.WriteLine($"Uploading {fileName} to S3 bucket {_settings.BucketName}.");
            // Use Amazon S3 SDK to upload the file from the stream
            // Example: TransferUtility.Upload(fileStream, _settings.BucketName, fileName);
            return Task.CompletedTask;
        }

        public Task<OutputResult> DownloadFileAsync(string fileId)
        {
            // Implement Amazon S3 file download logic using _settings
            Console.WriteLine($"Downloading {fileId} from S3 bucket {_settings.BucketName}.");
            return Task.FromResult<OutputResult>(null); // Replace with actual S3 SDK logic
        }
    }

}
