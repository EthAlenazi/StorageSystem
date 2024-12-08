using Microsoft.AspNetCore.Mvc;
using SimpleStorageService.Models;
using SimpleStorageService.Services;
using SimpleStorageService.Services.Helpers;
using System;
using System.Buffers.Text;
using System.Reflection.Emit;
using System.Text;

namespace SimpleBlobStore.Controllers
{
    [ApiController]
    [Route("v1/blobs")]
    public class BlobController : ControllerBase
    {
        private readonly StorageServices _storageService;

        public BlobController()
        {
            _storageService = new StorageServices();
        }

        [HttpPost]
        public IActionResult SaveBlob([FromBody] BlobModel blob)
        {
            if (blob.Id == 0 || string.IsNullOrEmpty(blob.DataBase64))
                return BadRequest("Invalid data provided.");

            try
            {
                // Use the CommonValidation helper
                if (!CommonValidation.IsValidBase64(blob.DataBase64))
                {
                    return BadRequest("Invalid input: Provided DataBase64 string is not in a valid Base64 format.");
                }

                var data = Convert.FromBase64String(blob.DataBase64);
                //If the service cannot decode the Base64 binary string upon receiving the request, the request should be rejected.
                _storageService.SaveBlob(blob.Id, data);

                return Ok(new { Message = "Blob stored successfully." });
            }

            catch (FormatException ex)
            {
                // Log the error (add appropriate logging here)
                return BadRequest("An error occurred while processing your request. Please try again.");
            }
            catch (Exception ex)
            {
                // Log the error (add appropriate logging here)
                return StatusCode(500, "An internal server error occurred. Please try again later.");
            }
        }
        
    

        [HttpGet("{id}")]
        public IActionResult GetBlob(int id)
        {
            var blob = _storageService.RetrieveBlob(id);
            if (blob == null)
                return NotFound("Blob not found.");

            var base64Data = Convert.ToBase64String(blob.Value.Data);
            return Ok(new
            {
                Id = id,
                Data = base64Data,
                Size = blob.Value.Data.Length,
                CreatedAt = blob.Value.CreatedAt.ToString("o") // ISO 8601 format
            });
        }
    }
}
