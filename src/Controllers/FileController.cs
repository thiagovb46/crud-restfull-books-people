using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.Business;
using src.Data.VO;

namespace src.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileBusiness _fileBusiness;

        public FileController(ILogger<FileController> logger, IFileBusiness fileBusiness)
        {
            _logger = logger;
            _fileBusiness = fileBusiness;
        }

        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType((200), Type = typeof (byte[]))]
        [Produces("application/octect-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName )
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);
            if(buffer!=null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".","")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("uploadFile")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType((200), Type = typeof (FileDetailsVo))]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file )
        {
            FileDetailsVo detail   = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost("uploadMultiplesFiles")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType((200), Type = typeof (List<FileDetailsVo>))]
        public async Task<IActionResult> UploadManyFiles([FromForm] List <IFormFile> files )
        {
            List<FileDetailsVo> details   = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }
    }
}