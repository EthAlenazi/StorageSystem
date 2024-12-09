using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Strategy.Implementation
{
    public class FtpStorage : IStorageStrategy
    {
        private readonly FtpSettings _settings;

        public FtpStorage(FtpSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task UploadFileAsync(string fileName, Guid fileId)
        {
            // Implement logic to upload file to FTP server
            Console.WriteLine($"Uploading {fileName} to FTP server {_settings.Host}:{_settings.Port} in directory {_settings.RootDirectory}.");
            return Task.CompletedTask; // Replace with actual FTP logic
        }

        public Task<ReturnFileModel> DownloadFileAsync(string fileId)
        {
            // Implement logic to download file from FTP server
            Console.WriteLine($"Downloading {fileId} from FTP server {_settings.Host}:{_settings.Port} in directory {_settings.RootDirectory}.");
            return Task.FromResult<ReturnFileModel>(null); // Replace with actual FTP logic
        }
    }


}
