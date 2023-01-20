using AutoMapper;
using Team.Application.Dtos;
using Team.Application.Helpers;
using Team.Domain.Entities;
using Team.Domain.Requests;

namespace Team.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(p => p.Manager.Name))
                .ForMember(dest => dest.ManagerEmail, opt => opt.MapFrom(p => p.Manager.Email))
                .ForMember(dest => dest.ManagerUsername, opt => opt.MapFrom(p => p.Manager.UserName))
                .ReverseMap();

            CreateMap<Project, CreateProjectRequest>().ReverseMap();
            CreateMap<Project, UpdateProjectRequest>().ReverseMap();

            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<Resource, CreateResourceRequest>().ReverseMap();
            CreateMap<Resource, UpdateResourceRequest>().ReverseMap();

            CreateMap<ProjectClient, ProjectClientDto>().ReverseMap();
            CreateMap<ProjectClient, CreateProjectClientRequest>().ReverseMap();
            CreateMap<ProjectClient, UpdateProjectClientRequest>().ReverseMap();

            CreateMap<ProjectServer, ProjectServerDto>().ReverseMap();
            CreateMap<ProjectServer, CreateProjectServerRequest>().ReverseMap();
            CreateMap<ProjectServer, UpdateProjectServerRequest>().ReverseMap();

            CreateMap<ProjectResource, ProjectResourceDto>().ReverseMap();
            CreateMap<ProjectResource, CreateProjectResourceRequest>().ReverseMap();
            CreateMap<ProjectResource, UpdateProjectResourceRequest>().ReverseMap();

            CreateMap<ProjectResourceDailyTask, ProjectResourceDailyTaskDto>().ReverseMap();
            CreateMap<ProjectResourceDailyTask, CreateProjectResourceDailyTaskRequest>().ReverseMap();
            CreateMap<ProjectResourceDailyTask, UpdateProjectResourceDailyTaskRequest>().ReverseMap();

            CreateMap<ProjectMilestone, ProjectMilestoneDto>().ReverseMap();
            //CreateMap<ProjectMilestone, CreateProjectMilestoneCommand>().ReverseMap();
            //CreateMap<ProjectMilestone, UpdateProjectMilestoneCommand>().ReverseMap();

            CreateMap<ProjectDocument, ProjectDocumentDto>()
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom<ProjectDocumentUrlResolver>())
                .ReverseMap();
            //CreateMap<ProjectDocument, CreateProjectDocumentCommand>().ReverseMap();
            //CreateMap<ProjectDocument, UpdateProjectDocumentCommand>().ReverseMap();
        }
    }
}
