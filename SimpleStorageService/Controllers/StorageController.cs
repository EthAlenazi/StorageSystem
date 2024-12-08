using Microsoft.AspNetCore.Mvc;
using SimpleStorageService.Services;
using SimpleStorageService.Services.Helpers;
using SimpleStorageService.Strategy.Interface;
using System.Buffers.Text;
using System.Reflection.Metadata;

namespace SimpleStorageService.Controllers
{
    [ApiController]
    [Route("api/storage")]
    public class StorageController : ControllerBase
    {
        private readonly IStorage _storage;

        public StorageController(IStorage storage)
        {
            _storage = storage;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(string file, string fileId)
        {
            if (!CommonValidation.IsValidBase64(file))
            {
                return BadRequest("Invalid input: Provided DataBase64 string is not in a valid Base64 format.");
            }
            await _storage.UploadFileAsync(file, file);
            return Ok("File uploaded successfully.");
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var fileStream = await _storage.DownloadFileAsync(fileName);
            return File(fileStream, "application/octet-stream", fileName);
        }
    }


    public class FileRequest
    {
        public string FileName { get; set; }
        public string FileData { get; set; }
    }

}
