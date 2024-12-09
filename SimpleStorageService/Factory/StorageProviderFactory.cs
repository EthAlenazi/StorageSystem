using Microsoft.Extensions.Options;
using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Implementation;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Factory
{
    public class StorageProviderFactory
    {
        private readonly StorageSettings _settings;

        public StorageProviderFactory(IOptions<StorageSettings> options)
        {
            _settings = options.Value;
        }
        public IEnumerable<IStorageStrategy> CreateStorages(IEnumerable<string> storageTypes = null)
        {
            var types = storageTypes ?? new List<string> { _settings.DefaultStorage };

            return types.Select(type => (IStorageStrategy)(type switch
            {
                "AmazonS3" => new S3Storage(_settings.AmazonS3),
                "Database" => new DatabaseStorage(_settings.Database),
                "LocalFileSystem" => new LocalFileSystemStorage(_settings.LocalFileSystem),
                "FTP" => new FtpStorage(_settings.FTP),
                _ => throw new ArgumentException($"Unknown storage type: {type}")
            }));
        }

    }

}
