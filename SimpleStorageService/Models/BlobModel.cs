namespace SimpleStorageService.Models
{

    public class SaveFileModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Data { get; set; }
    }
    public class BlobModel
    {
        public int Id { get; set; }//guid
        public string DataBase64 { get; set; }
    }

}
