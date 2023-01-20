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
    [Route("api/projects/{projectResourceId}/[controller]")]
    [ApiController]
    public class ProjectResourceDailyTasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectResourceRepository _projectResourceRepository;
        private readonly IProjectResourceDailyTaskRepository _projectResourceDailyTaskRepository;

        public ProjectResourceDailyTasksController(IMapper mapper, IProjectResourceRepository projectResourceRepository, IProjectResourceDailyTaskRepository projectResourceDailyTaskRepository)
        {
            _mapper = mapper;
            _projectResourceRepository = projectResourceRepository;
            _projectResourceDailyTaskRepository = projectResourceDailyTaskRepository;
        }

        /// <summary>
        /// Get specific project resource daily task by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectResourceDailyTaskId}")]
        [ProducesResponseType(typeof(ProjectResourceDailyTaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectResourceDailyTaskDto>> Get(Guid projectResourceId, Guid projectResourceDailyTaskId)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(projectResourceId);
            if (entityProjectResource == null)
            {
                throw new NotFoundException(nameof(ProjectResource), projectResourceId);
            }

            var entity = await _projectResourceDailyTaskRepository.GetByIdAsync(projectResourceDailyTaskId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ProjectResourceDailyTask), projectResourceDailyTaskId);
            }

            return Ok(_mapper.Map<ProjectResourceDailyTaskDto>(entity));
        }

        /// <summary>
        /// Get list of all daily tasks for the specific project resource
        /// </summary>
        /// <param name="projectResourceId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectResourceDailyTaskDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectResourceDailyTaskDto>>> GetByUser(Guid projectResourceId)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(projectResourceId);
            if (entityProjectResource == null)
            {
                throw new NotFoundException(nameof(ProjectResource), projectResourceId);
            }

            var projectResourceDailyTasks = await _projectResourceDailyTaskRepository.GetAsync(p => p.ProjectResourceId == projectResourceId);

            return Ok(_mapper.Map<IEnumerable<ProjectResourceDailyTaskDto>>(projectResourceDailyTasks));
        }

        /// <summary>
        /// Registers a new daily task for project resource
        /// </summary>
        /// <param name="projectResourceId">Project resource id</param>
        /// <param name="request">Project resource data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(Guid projectResourceId, [FromBody] CreateProjectResourceDailyTaskRequest request)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(projectResourceId);
            if (entityProjectResource == null)
            {
                throw new NotFoundException(nameof(ProjectResource), projectResourceId);
            }

            var entity = _mapper.Map<ProjectResourceDailyTask>(request);

            var newEntity = await _projectResourceDailyTaskRepository.AddAsync(entity);

            return Ok(newEntity.Id);
        }

        /// <summary>
        /// Update an existing daily task for the project resource
        /// </summary>
        /// <param name="projectResourceId">Project ID</param>
        /// <param name="request">Project resource daily task data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectResourceId, [FromBody] UpdateProjectResourceDailyTaskRequest request)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(request.ProjectResourceId);
            if (entityProjectResource == null)
            {
                throw new NotFoundException(nameof(ProjectResource), request.ProjectResourceId);
            }

            var entityToUpdate = await _projectResourceDailyTaskRepository.GetByIdAsync(request.ProjectResourceDailyTaskId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(ProjectResourceDailyTask), request.ProjectResourceDailyTaskId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectResourceDailyTaskRequest), typeof(ProjectResourceDailyTask));

            await _projectResourceDailyTaskRepository.UpdateAsync(entityToUpdate);
            return NoContent();
        }

        /// <summary>
        /// Deletes / Removes an existing daily task for project resource
        /// </summary>
        /// <param name="projectResourceId"></param>
        /// <param name="projectResourceDailyTaskId"></param>
        /// <returns></returns>
        [HttpDelete("{projectResourceDailyTaskId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid projectResourceId, Guid projectResourceDailyTaskId)
        {
            var entityProjectResource = await _projectResourceRepository.GetByIdAsync(projectResourceId);
            if (entityProjectResource == null)
            {
                throw new NotFoundException(nameof(ProjectResource), projectResourceId);
            }

            var entityToDelete = await _projectResourceDailyTaskRepository.GetByIdAsync(projectResourceDailyTaskId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(ProjectResourceDailyTask), projectResourceDailyTaskId);
            }

            await _projectResourceDailyTaskRepository.DeleteAsync(entityToDelete);
            return NoContent();
        }
    }
}
