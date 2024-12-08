using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Strategy.Implementation
{
    public class DatabaseStorage : IStorage
    {
        private readonly DatabaseSettings _settings;

        public DatabaseStorage(DatabaseSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task UploadFileAsync(string fileName, string fileStream)
        {
            // Implement logic to store file in database table
            Console.WriteLine($"Storing {fileName} into database table {_settings.TableName}.");
            return Task.CompletedTask; // Replace with actual database logic
        }

        public Task<Stream> DownloadFileAsync(string fileName)
        {
            // Implement logic to retrieve file from database table
            Console.WriteLine($"Retrieving {fileName} from database table {_settings.TableName}.");
            return Task.FromResult<Stream>(null); // Replace with actual database logic
        }
    }


}
