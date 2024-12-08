using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Implementation;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Services
{
    public class StorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<object> UploadFile(string fileName, string fileData)
        {
            // Call the appropriate storage backend's save method
            await _storage.UploadFileAsync(fileName, fileData);

            // Return a result based on the storage backend
            return _storage switch
            {
                S3Storage => new { message = "File uploaded to S3", fileUrl = "https://s3.amazonaws.com/your-bucket/" + fileName },
                DatabaseStorage => new { message = "File uploaded to database", fileId = 12345 },
                LocalFileStorage => new { message = "File uploaded to local storage", filePath = "C:/files/" + fileName },
                FtpStorage => new { message = "File uploaded to FTP", remoteFilePath = "/uploads/" + fileName },
                _ => new { message = "Unknown storage backend" }
            };
        }

        public async Task<object> GetFile(string fileName)
        {
            // Retrieve the file from the appropriate storage backend
            var fileData = await _storage.DownloadFileAsync(fileName);

            // Return the file data or metadata
            return _storage switch
            {
                S3Storage => fileData,  // Return file content (or URL)
                DatabaseStorage => fileData,  // Return file data or metadata
                LocalFileStorage => fileData,  // Return file data or metadata
                FtpStorage => fileData,  // Return file data or metadata
                _ => new { message = "Unknown storage backend" }
            };
        }
    }



}
