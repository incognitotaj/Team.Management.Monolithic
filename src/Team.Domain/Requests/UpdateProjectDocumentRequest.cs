using Microsoft.AspNetCore.Http;

namespace Team.Domain.Requests
{
    public class UpdateProjectDocumentRequest
    {
        public Guid ProjectDocumentId { get; set; }
        public string Title { get; set; }
        public IFormFile Document { get; set; }
        public string Detail { get; set; }
    }
}
