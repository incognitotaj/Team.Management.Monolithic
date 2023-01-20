using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;
using Team.Domain.Requests;

namespace Team.API.Controllers
{
    [Route("api/projects/{projectId}/[controller]")]
    [ApiController]
    public class ProjectClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectClientRepository _projectClientRepository;

        public ProjectClientsController(IMapper mapper, IProjectClientRepository projectClientRepository)
        {
            _mapper = mapper;
            _projectClientRepository = projectClientRepository;
        }

        /// <summary>
        /// Get specific project client by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectClientId}")]
        [ProducesResponseType(typeof(ProjectClientDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectClientDto>> Get(Guid projectId, Guid projectClientId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entity = await _projectClientRepository.GetByIdAsync(projectClientId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ProjectClient), projectClientId);
            }

            return Ok(_mapper.Map<ProjectClientDto>(entity));
        }

        /// <summary>
        /// Get list of all project clients for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectClientDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectClientDto>>> GetByUser(Guid projectId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var projectClients = await _projectClientRepository.GetAsync(p => p.ProjectId == projectId);

            return Ok(_mapper.Map<IEnumerable<ProjectClientDto>>(projectClients));
        }

        /// <summary>
        /// Creates a new project client for the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project client data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(string projectId, [FromBody] CreateProjectClientRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = _mapper.Map<ProjectClient>(request);

            var newEntity = await _projectClientRepository.AddAsync(entity);

            return Ok(newEntity.Id);
        }

        /// <summary>
        /// Update an existing project client
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project client data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectId, [FromBody] UpdateProjectClientRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToUpdate = await _projectClientRepository.GetByIdAsync(request.ProjectClientId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(ProjectClient), request.ProjectClientId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectClientRequest), typeof(ProjectClient));

            await _projectClientRepository.UpdateAsync(entityToUpdate);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project client
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectClientId"></param>
        /// <returns></returns>
        [HttpDelete("{projectClientId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid projectId, Guid projectClientId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToDelete = await _projectClientRepository.GetByIdAsync(projectClientId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(ProjectClient), projectClientId);
            }

            await _projectClientRepository.DeleteAsync(entityToDelete);
            return NoContent();
        }
    }
}
