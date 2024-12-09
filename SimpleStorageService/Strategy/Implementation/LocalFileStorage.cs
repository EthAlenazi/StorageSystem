using SimpleStorageService.Models;
using SimpleStorageService.Strategy.Interface;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleStorageService.Strategy.Implementation
{
    public class LocalFileStorage : IStorage
    {
        private readonly LocalFileSystemSettings _settings;
        private readonly string _storagePath;


        public LocalFileStorage(LocalFileSystemSettings settings)
        {
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
        }

        public Task UploadFileAsync(string fileData, Guid fileId)
        {
            var filePath = Path.Combine(_storagePath, fileId.ToString());
            var data = Convert.FromBase64String(fileData);
            File.WriteAllBytes(filePath, data); 
            return Task.CompletedTask;
        }

        public Task<OutputResult> DownloadFileAsync(string fileId)
        {
            
            var filePath = Path.Combine(_storagePath, fileId.ToString());
            if (!File.Exists(filePath))
                return null;

            var data = File.ReadAllBytes(filePath);
            var createdAt = File.GetCreationTimeUtc(filePath);
            var base64Data = Convert.ToBase64String(data);
            var outputResult = new OutputResult()
            {
                Id = fileId,
                Data = base64Data,
                Created_at = createdAt,
                Size= data.Length
            };

            return Task.FromResult<OutputResult>(outputResult);
        }
    }


}
