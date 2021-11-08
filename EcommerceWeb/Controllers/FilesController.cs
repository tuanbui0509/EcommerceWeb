using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EcommerceSolution.Application.Common;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IBlobService _blobService;

        public FilesController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet("GetBlobsAsync")]
        public async Task<IActionResult> GetBlobsAsync()
        {
            var files = await _blobService.GetBlobsAsync("images");
            return Ok(files);
        }

        [HttpGet("GetBlobAsync")]
        public async Task<IActionResult> GetBlobAsync(string name)
        {
            var res = await _blobService.GetBlobAsync(name, "images");

            return Ok(res);
        }

        [HttpGet("DeleteBlobAsync")]
        public async Task<IActionResult> DeleteBlobAsync(string name)
        {
            await _blobService.DeleteBlobAsync(name, "images");

            return Ok("Delete Successful");
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if (file == null || file.Length <= 0) return BadRequest("Error File");

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName.Replace(" ", ""));
                var res = await _blobService.UploadFileBlobAsync(fileName, file, "images");

                if (res)
                    return Ok(res);
            }
            return Ok();
        }
    }
}
