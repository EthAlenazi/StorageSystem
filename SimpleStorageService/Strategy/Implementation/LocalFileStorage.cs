﻿using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Strategy.Implementation
{
    public class LocalFileStorage : IStorage
    {
        private readonly LocalFileSystemSettings _settings;

        public LocalFileStorage(LocalFileSystemSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task UploadFileAsync(string fileName, string fileStream)
        {
            Console.WriteLine($"Uploading {fileName} to Local File bucket {_settings.RootPath}.");
            // Use Amazon S3 SDK to upload the file from the stream
            // Example: TransferUtility.Upload(fileStream, _settings.BucketName, fileName);
            return Task.CompletedTask;
        }

        public Task<Stream> DownloadFileAsync(string fileName)
        {
            var filePath = Path.Combine(_settings.RootPath, fileName);
            Console.WriteLine($"Reading file from local path: {filePath}");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", fileName);
            }

            return Task.FromResult<Stream>(new FileStream(filePath, FileMode.Open, FileAccess.Read));
        }
    }


}
