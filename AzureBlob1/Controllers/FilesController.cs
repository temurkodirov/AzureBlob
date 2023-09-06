﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlob1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly FileService _fileService;

        public FilesController(FileService fileService)
        {
            _fileService = fileService;
        }


        [HttpGet]
        public async Task<IActionResult> ListAllblobs()
        {
            var result = await _fileService.ListAsync();
            return Ok(result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var response = await _fileService.UploadAsync(file);

            if (response.Error)
            {
                return BadRequest(response.Status);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = _fileService.UploadAsync(file);
            return Ok(result);
        }


        [HttpGet]
        [Route("filename")]
        public async Task<IActionResult> Download(string filename)
        {
            var result = await _fileService.DownlaodAsync(filename);
            return File(result.Content, result.ContentType, result.Name);
        }


        [HttpDelete]
        [Route("filename")]
        public async Task<IActionResult> Delete(string filename)
        {
            var result = await _fileService.DeleteAsync(filename);
            return Ok(result);
        }
    }
}
