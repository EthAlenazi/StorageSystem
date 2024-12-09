using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Strategy.Implementation
{
    public class DatabaseStorage : IStorageStrategy
    {
        private readonly DatabaseSettings _settings;

        public DatabaseStorage(DatabaseSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task UploadFileAsync(string fileName, Guid fileId)
        {
            // Implement logic to store file in database table
            Console.WriteLine($"Storing {fileName} into database table {_settings.TableName}.");
            return Task.CompletedTask; // Replace with actual database logic
        }

        public Task<ReturnFileModel> DownloadFileAsync(string fileId)
        {
            // Implement logic to retrieve file from database table
            Console.WriteLine($"Retrieving {fileId} from database table {_settings.TableName}.");
            return Task.FromResult<ReturnFileModel>(null); // Replace with actual database logic
        }
    }


}
