namespace SimpleStorageService.Models
{
    public class StorageSettings
    {
        public string DefaultStorage { get; set; }
        public AmazonS3Settings AmazonS3 { get; set; }
        public DatabaseSettings Database { get; set; }
        public LocalFileSystemSettings LocalFileSystem { get; set; }
        public FtpSettings FTP { get; set; }
    }

    public class AmazonS3Settings
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string Region { get; set; }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
    }

    public class LocalFileSystemSettings
    {
        public string RootPath { get; set; }
    }

    public class FtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RootDirectory { get; set; }
    }

}
