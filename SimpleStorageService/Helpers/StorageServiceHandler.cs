using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;

namespace SimpleStorageService.Helpers
{
    public class StorageServiceHandler
    {
        private readonly IEnumerable<IStorageStrategy> _storages;

        public StorageServiceHandler(IEnumerable<IStorageStrategy> storages)
        {
            _storages = storages;
        }

        public async Task HandleUploadAsync(SaveFileModel model)
        {
            foreach (var storage in _storages)
            {
                await storage.UploadFileAsync(model.Data, model.Id);
            }
        }

        public async Task<ReturnFileModel> HandleDownloadAsync(string fileId)
        {
            var downloadTasks = _storages.Select(storage =>
            {
                return Task.Run(async () =>
                {
                    try
                    {
                        return await storage.DownloadFileAsync(fileId);
                    }
                    catch
                    {
                        return null;
                    }
                });
            });

            var completedTask = await Task.WhenAny(downloadTasks);
            var result = await completedTask;

            if (result == null)
                throw new Exception("File not found in any storage");

            return result;
        }
    }

}
