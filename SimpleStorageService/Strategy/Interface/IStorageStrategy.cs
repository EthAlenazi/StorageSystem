using SimpleStorageService.Models;

namespace SimpleStorageService.Strategy.Interface
{
    public interface IStorageStrategy
    {
        Task UploadFileAsync(string fileData, Guid fileId);
        Task<ReturnFileModel> DownloadFileAsync(string fileId);
    }

}
