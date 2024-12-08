namespace SimpleStorageService.Strategy.Interface
{
    public interface IStorage
    {
        Task UploadFileAsync(string fileName, string fileStream);
        Task<Stream> DownloadFileAsync(string fileName);
    }

}
