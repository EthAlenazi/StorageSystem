using Microsoft.Extensions.Options;
using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Implementation;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Factory
{
    public class StorageFactory
    {
        private readonly StorageSettings _settings;

        public StorageFactory(IOptions<StorageSettings> options)
        {
            _settings = options.Value;
        }

        public IStorage CreateStorage(string storageType = null)
        {
            var type = storageType ?? _settings.DefaultStorage;

            return type switch
            {
                "S3" => new S3Storage(_settings.AmazonS3),
                "Database" => new DatabaseStorage(_settings.Database),
                "LocalFileSystem" => new LocalFileStorage(_settings.LocalFileSystem),
                "FTP" => new FtpStorage(_settings.FTP),
                _ => throw new ArgumentException($"Unknown storage type: {type}")
            };
        }
    }

}
