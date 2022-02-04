using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using src.Data.VO;

namespace src.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailsVo> SaveFileToDisk(IFormFile file);
        public Task <List<FileDetailsVo>> SaveFilesToDisk(IList<IFormFile> file);
        
    }
}