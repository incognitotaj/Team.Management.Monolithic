using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;
using Team.Domain.Requests;

namespace Team.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
         
        public ProjectsController(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Get list of all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var projects = await _projectRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projects));
        }

        /// <summary>
        /// Get a single specific project by it's unique id (GUID)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(ProjectDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectDto>> GetById(Guid projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if(project == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProjectDto>(project));
        }

        /// <summary>
        /// Get list of all projects for the specific user as manager
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("get-by-user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<ProjectDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetByUser(Guid userId)
        {
            var resultList = await _projectRepository.GetProjectsByUserAsync(userId);
            return Ok(_mapper.Map<List<ProjectDto>>(resultList));
        }

        /// <summary>
        /// Creates / registers a new project
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProjectRequest request)
        {
            var entity = _mapper.Map<Project>(request);

            var newEntity = await _projectRepository.AddAsync(entity);

            return Ok(newEntity.Id);
        }

        /// <summary>
        /// Updates an existing project
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update([FromBody] UpdateProjectRequest request)
        {
            var entityToUpdate = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            await _projectRepository.UpdateAsync(entityToUpdate);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid projectId)
        {
            var entityToDelete = await _projectRepository.GetByIdAsync(projectId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            await _projectRepository.DeleteAsync(entityToDelete);
            return NoContent();
        }
    }
}
