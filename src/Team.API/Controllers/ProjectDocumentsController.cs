using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.Application.Contracts.Persistence;
using Team.Application.Contracts.Services;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;
using Team.Domain.Requests;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectDocumentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectDocumentRepository _projectDocumentRepository;
        private readonly IFileUploadOnServerService _fileUploadOnServerService;

        public ProjectDocumentsController(IMapper mapper, IProjectRepository projectRepository, IProjectDocumentRepository projectDocumentRepository, IFileUploadOnServerService fileUploadOnServerService)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectDocumentRepository = projectDocumentRepository;
            _fileUploadOnServerService = fileUploadOnServerService;
        }

        /// <summary>
        /// Get specific project document by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectDocumentId}")]
        [ProducesResponseType(typeof(ProjectDocumentDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectDocumentDto>> Get(Guid projectId, Guid projectDocumentId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entity = await _projectDocumentRepository.GetByIdAsync(projectDocumentId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ProjectDocument), projectDocumentId);
            }

            return Ok(_mapper.Map<ProjectDocumentDto>(entity));
        }

        /// <summary>
        /// Get list of all project documents for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectDocumentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectDocumentDto>>> GetByProject(Guid projectId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var projectDocuments = await _projectDocumentRepository.GetAsync(p => p.ProjectId == projectId);

            return Ok(_mapper.Map<IEnumerable<ProjectDocumentDto>>(projectDocuments));
        }

        /// <summary>
        /// Creates a new project client for the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project client data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(Guid projectId, [FromForm] CreateProjectDocumentRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            FileInfo fi = new FileInfo(request.Document.FileName);

            var projectName = entityProject.Id.ToString();
            var basePath = "documents";
            var fileName = DateTime.Now.Ticks.ToString();
            var url = $"/{basePath}/{projectName}/{fileName}{fi.Extension}";

            if (await _fileUploadOnServerService.UploadFile(
                file: request.Document,
                baseUrl: basePath,
                directoryName: projectName,
                fileName: fileName,
                fileExtension: fi.Extension))
            {
                var entity = new ProjectDocument
                (
                    title: request.Title,
                    filePath: url,
                    detail: request.Detail,
                    projectId: projectId
                );

                var newEntity = await _projectDocumentRepository.AddAsync(entity);

                return newEntity.Id;
            }

            throw new Exception("Error while saving file on server");
        }

        /// <summary>
        /// Update an existing project Document
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project Document data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(Guid projectId, [FromForm] UpdateProjectDocumentRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToUpdate = await _projectDocumentRepository.GetByIdAsync(request.ProjectDocumentId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(ProjectDocument), request.ProjectDocumentId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectDocumentRequest), typeof(ProjectDocument));
            entityToUpdate.ProjectId = projectId;

            if (request.Document != null)
            {
                FileInfo fi = new FileInfo(request.Document.FileName);

                var projectName = entityProject.Id.ToString();
                var basePath = "documents";
                var fileName = DateTime.Now.Ticks.ToString();
                var url = $"/{basePath}/{projectName}/{fileName}{fi.Extension}";

                await _fileUploadOnServerService.UploadFile(
                    file: request.Document,
                    baseUrl: basePath,
                    directoryName: projectName,
                    fileName: fileName,
                    fileExtension: fi.Extension);

                entityToUpdate.FilePath = url;
            }

            await _projectDocumentRepository.UpdateAsync(entityToUpdate);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project Document
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectDocumentId"></param>
        /// <returns></returns>
        [HttpDelete("{projectDocumentId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid projectId, Guid projectDocumentId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToDelete = await _projectDocumentRepository.GetByIdAsync(projectDocumentId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(ProjectDocument), projectDocumentId);
            }

            await _projectDocumentRepository.DeleteAsync(entityToDelete);
            return NoContent();
        }
    }
}
