using AutoMapper;
using Azure.Core;
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
    public class ProjectResourcesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectResourceRepository _projectResourceRepository;

        public ProjectResourcesController(IMapper mapper, IProjectRepository projectRepository, IProjectResourceRepository projectResourceRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectResourceRepository = projectResourceRepository;
        }

        /// <summary>
        /// Get specific project resource by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectResourceId}")]
        [ProducesResponseType(typeof(ProjectResourceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectResourceDto>> Get(Guid projectId, Guid projectResourceId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entity = await _projectResourceRepository.GetByIdAsync(projectResourceId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ProjectResource), projectResourceId);
            }

            return Ok(_mapper.Map<ProjectResourceDto>(entity));
        }

        /// <summary>
        /// Get list of all project resources for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectResourceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectResourceDto>>> GetByProject(Guid projectId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var projectResources = await _projectResourceRepository.GetAsync(p => p.ProjectId == projectId);

            return Ok(_mapper.Map<IEnumerable<ProjectResourceDto>>(projectResources));
        }

        /// <summary>
        /// Registers / Assigns a new resource to the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project resource data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(Guid projectId, [FromBody] CreateProjectResourceRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entity = _mapper.Map<ProjectResource>(request);
            entity.ProjectId = projectId;

            var newEntity = await _projectResourceRepository.AddAsync(entity);

            return Ok(newEntity.Id);
        }

        /// <summary>
        /// Update an existing project resource
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project resource data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(Guid projectId, [FromBody] UpdateProjectResourceRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToUpdate = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectResourceRequest), typeof(ProjectResource));
            entityToUpdate.ProjectId = projectId;

            await _projectResourceRepository.UpdateAsync(entityToUpdate);

            return NoContent();
        }

        /// <summary>
        /// Deletes / Removes an existing resource from project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectResourceId"></param>
        /// <returns></returns>
        [HttpDelete("{projectResourceId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid projectId, Guid projectResourceId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToDelete = await _projectResourceRepository.GetByIdAsync(projectResourceId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(ProjectResource), projectResourceId);
            }
            await _projectResourceRepository.DeleteAsync(entityToDelete);

            return NoContent();
        }
    }
}
