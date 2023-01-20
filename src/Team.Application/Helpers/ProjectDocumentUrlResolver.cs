using AutoMapper;
using Microsoft.Extensions.Configuration;
using Team.Application.Dtos;
using Team.Domain.Entities;

namespace Team.Application.Helpers
{
    public class ProjectDocumentUrlResolver : IValueResolver<ProjectDocument, ProjectDocumentDto, string>
    {
        private readonly IConfiguration _config;

        public ProjectDocumentUrlResolver(IConfiguration config)
        {
            _config = config;
        }


        public string Resolve(ProjectDocument source, ProjectDocumentDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FilePath))
            {
                return _config["ApiUrl"] + source.FilePath;
            }

            return null;
        }
    }
}
