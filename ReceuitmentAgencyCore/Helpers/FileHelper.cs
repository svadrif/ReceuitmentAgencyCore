using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Helpers
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileHelper> _logger;
        public FileHelper(IWebHostEnvironment webHostEnvironment, ILogger<FileHelper> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            try
            {
                string path = "/Files/" + Guid.NewGuid() + file.FileName;
                await using FileStream fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return path;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return string.Empty;
            }      
        }
    }
}