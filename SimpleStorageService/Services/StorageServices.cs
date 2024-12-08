namespace SimpleStorageService.Services
{
    public class StorageServices
    {
        private readonly string _storagePath;

        public StorageServices()
        {
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
        }

        public void SaveBlob(int id, byte[] data)
        {
            var filePath = Path.Combine(_storagePath, id.ToString());
            File.WriteAllBytes(filePath, data); // حفظ الملف كملف بايت
        }

        public (byte[] Data, DateTime CreatedAt)? RetrieveBlob(int id)
        {
            var filePath = Path.Combine(_storagePath, id.ToString());
            if (!File.Exists(filePath))
                return null;

            var data = File.ReadAllBytes(filePath);
            var createdAt = File.GetCreationTimeUtc(filePath);
            return (data, createdAt);
        }
    }
}
