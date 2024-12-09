using Microsoft.AspNetCore.Mvc;
using SimpleStorageService.Helpers;
using SimpleStorageService.Models;
using SimpleStorageService.Validation;

namespace SimpleStorageService.Controllers
{
    [ApiController]
    [Route("api/storage")]
    public class StorageController : ControllerBase
    {
        private readonly StorageServiceHandler _storageHandler;

        public StorageController(StorageServiceHandler storageHandler)
        {
            _storageHandler = storageHandler;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(SaveFileModel model)
        {
            if (!CommonValidation.IsValidBase64(model.Data))
            {
                return BadRequest("Invalid input: Provided DataBase64 string is not in a valid Base64 format.");
            }

            await _storageHandler.HandleUploadAsync(model);

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

            return Ok(fileStream);
        }


    }

}
