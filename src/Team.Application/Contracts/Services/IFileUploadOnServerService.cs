using Microsoft.AspNetCore.Http;

namespace Team.Application.Contracts.Services
{
    public interface IFileUploadOnServerService
    {
        Task<bool> UploadFile(IFormFile file, string baseUrl, string directoryName, string fileName, string fileExtension);
    }
}
