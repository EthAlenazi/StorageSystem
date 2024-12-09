namespace SimpleStorageService.Models
{

    public class SaveFileModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Data { get; set; }
    }
    

}
