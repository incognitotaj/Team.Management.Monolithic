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
            //CreateMap<ProjectClient, CreateProjectClientCommand>().ReverseMap();
            //CreateMap<ProjectClient, UpdateProjectClientCommand>().ReverseMap();

            CreateMap<ProjectServer, ProjectServerDto>().ReverseMap();
            //CreateMap<ProjectServer, CreateProjectServerCommand>().ReverseMap();
            //CreateMap<ProjectServer, UpdateProjectServerCommand>().ReverseMap();

            CreateMap<ProjectResource, ProjectResourceDto>().ReverseMap();
            //CreateMap<ProjectResource, CreateProjectResourceCommand>().ReverseMap();
            //CreateMap<ProjectResource, UpdateProjectResourceCommand>().ReverseMap();

            CreateMap<ProjectResourceDailyTask, ProjectResourceDailyTaskDto>().ReverseMap();
            //CreateMap<ProjectResourceDailyTask, CreateProjectResourceDailyTaskCommand>().ReverseMap();
            //CreateMap<ProjectResourceDailyTask, UpdateProjectResourceDailyTaskCommand>().ReverseMap();

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
