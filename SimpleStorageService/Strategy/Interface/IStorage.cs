using SimpleStorageService.Models;

namespace SimpleStorageService.Strategy.Interface
{
    public interface IStorage
    {
        Task UploadFileAsync(string fileData, Guid fileId);
        Task<OutputResult> DownloadFileAsync(string fileId);
    }

}
