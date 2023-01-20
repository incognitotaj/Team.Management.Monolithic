using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Team.Application.Contracts.Services;

namespace Team.Infrastructure.Services
{
    public class FileUploadOnServerService : IFileUploadOnServerService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileUploadOnServerService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<bool> UploadFile(IFormFile file, string baseUrl, string directoryName, string fileName, string fileExtension)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(_hostEnvironment.WebRootPath, baseUrl, directoryName));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, fileName + fileExtension), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
