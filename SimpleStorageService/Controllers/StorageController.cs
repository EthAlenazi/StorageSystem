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
        private readonly StorageHandler _storageHandler;

        public StorageController(StorageHandler storageHandler)
        {
            _storageHandler = storageHandler;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(string file, string fileId)
        {
            if (!CommonValidation.IsValidBase64(file))
            {
                return BadRequest("Invalid input: Provided DataBase64 string is not in a valid Base64 format.");
            }

            await _storageHandler.HandleUploadAsync(fileId, file);

            return Ok($"File uploaded successfully to all storages.");
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var fileStream = await _storageHandler.HandleDownloadAsync(fileName);
            if (fileStream == null)
            {
                return NotFound("File not found in any storage.");
            }

            return File(fileStream, "application/octet-stream", fileName);
        }


    }

}
