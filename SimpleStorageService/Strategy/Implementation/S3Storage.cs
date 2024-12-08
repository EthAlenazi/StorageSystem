using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Strategy.Implementation
{
    public class S3Storage : IStorage
    {
        private readonly AmazonS3Settings _settings;

        public S3Storage(AmazonS3Settings settings)
        {
            _settings = settings;
        }

        public Task UploadFileAsync(string fileName, string fileStream)
        {
            //    // Implement Amazon S3 file upload logic using _settings
            //    Console.WriteLine($"Uploading {fileName} to S3 bucket {_settings.BucketName} in region {_settings.Region}.");
            //    return Task.CompletedTask; // Replace with actual S3 SDK logic
            //
            Console.WriteLine($"Uploading {fileName} to S3 bucket {_settings.BucketName}.");
            // Use Amazon S3 SDK to upload the file from the stream
            // Example: TransferUtility.Upload(fileStream, _settings.BucketName, fileName);
            return Task.CompletedTask;
        }

        public Task<Stream> DownloadFileAsync(string fileName)
        {
            // Implement Amazon S3 file download logic using _settings
            Console.WriteLine($"Downloading {fileName} from S3 bucket {_settings.BucketName}.");
            return Task.FromResult<Stream>(null); // Replace with actual S3 SDK logic
        }
    }

}
