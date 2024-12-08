using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Services.Helpers
{
    public class StorageHandler
    {
        private readonly IEnumerable<IStorage> _storages;

        public StorageHandler(IEnumerable<IStorage> storages)
        {
            _storages = storages;
        }

        public async Task HandleUploadAsync(string fileName, string fileContent)
        {
            foreach (var storage in _storages)
            {
                await storage.UploadFileAsync(fileName, fileContent);
            }
        }

        public async Task<Stream> HandleDownloadAsync(string filePath)
        {
            var downloadTasks = _storages.Select(storage =>
            {
                return Task.Run(async () =>
                {
                    try
                    {
                        return await storage.DownloadFileAsync(filePath);
                    }
                    catch
                    {
                        return null; // Ignore errors for unavailable storages
                    }
                });
            });

            var completedTask = await Task.WhenAny(downloadTasks);
            var result = await completedTask;

            if (result == null)
                throw new Exception("File not found in any storage");

            return result;
        }

        //public async Task HandleUploadSelectiveAsync(string fileName, Stream fileContent, IEnumerable<string> selectedStorageTypes)
        //{
        //    var filteredStorages = _storages.Where(s => selectedStorageTypes.Contains(s.StorageType));

        //    foreach (var storage in filteredStorages)
        //    {
        //        await storage.UploadFileAsync(fileName, fileContent);
        //    }
        //}
    }

}
