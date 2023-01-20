using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class ProjectMilestonesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMilestoneRepository _projectMilestoneRepository;

        public ProjectMilestonesController(IMapper mapper, IProjectRepository projectRepository, IProjectMilestoneRepository projectMilestoneRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectMilestoneRepository = projectMilestoneRepository;
        }

        /// <summary>
        /// Get specific project milestone by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectMilestoneId}")]
        [ProducesResponseType(typeof(ProjectMilestoneDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectMilestoneDto>> Get(Guid projectId, Guid projectMilestoneId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entity = await _projectMilestoneRepository.GetByIdAsync(projectMilestoneId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ProjectMilestone), projectMilestoneId);
            }

            return Ok(_mapper.Map<ProjectMilestoneDto>(entity));
        }

        /// <summary>
        /// Get list of all project milestones for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectMilestoneDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectMilestoneDto>>> GetByProject(Guid projectId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var projectMilestones = await _projectMilestoneRepository.GetAsync(p => p.ProjectId == projectId);

            return Ok(_mapper.Map<IEnumerable<ProjectMilestoneDto>>(projectMilestones));
        }

        /// <summary>
        /// Creates a new project client for the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project milestone data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(Guid projectId, [FromBody] CreateProjectMilestoneRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entity = _mapper.Map<ProjectMilestone>(request);
            entity.ProjectId = projectId;

            var newEntity = await _projectMilestoneRepository.AddAsync(entity);

            return Ok(newEntity.Id);
        }

        /// <summary>
        /// Update an existing project milestone
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project client data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(Guid projectId, [FromBody] UpdateProjectMilestoneRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToUpdate = await _projectMilestoneRepository.GetByIdAsync(request.ProjectMilestoneId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(ProjectMilestone), request.ProjectMilestoneId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectMilestoneRequest), typeof(ProjectMilestone));
            entityToUpdate.ProjectId = projectId;

            await _projectMilestoneRepository.UpdateAsync(entityToUpdate);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project milestone
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectMilestoneId"></param>
        /// <returns></returns>
        [HttpDelete("{projectMilestoneId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid projectId, Guid projectMilestoneId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToDelete = await _projectMilestoneRepository.GetByIdAsync(projectMilestoneId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(ProjectMilestone), projectMilestoneId);
            }

            await _projectMilestoneRepository.DeleteAsync(entityToDelete);
            return NoContent();
        }
    }
}
