using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using src.Data.VO;

namespace src.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor  _context;
        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory()+"\\Upload\\";
            
        }
        public async Task<FileDetailsVo> SaveFileToDisk(IFormFile file)
        {
            FileDetailsVo fileDetails =  new FileDetailsVo ();
            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;
            
            if(fileType.ToLower()==".pdf" 
            ||  fileType.ToLower()==".jpg"
            || fileType.ToLower()==".png" 
            || fileType.ToLower()==".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);
                if(file!=null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath,"",docName);
                    fileDetails.DocumentName = docName;
                    fileDetails.DocType = fileType;
                    fileDetails.DocUrl = Path.Combine(baseUrl+ "/api/file/v1" + fileDetails.DocumentName);
                    
                    using var stream  = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return fileDetails;
        }
        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;
            return File.ReadAllBytes(filePath);
        }

        public async Task<List<FileDetailsVo>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailsVo> list = new List<FileDetailsVo>();
            foreach(var file in files)
            {
                list.Add(await this.SaveFileToDisk(file));
            }
            return list;
        }

        
    }
}