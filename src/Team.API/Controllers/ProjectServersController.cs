﻿using AutoMapper;
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
    public class ProjectServersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectServerRepository _projectServerRepository;

        public ProjectServersController(IMapper mapper, IProjectRepository projectRepository, IProjectServerRepository projectServerRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _projectServerRepository = projectServerRepository;
        }

        /// <summary>
        /// Get specific project Server by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{projectServerId}")]
        [ProducesResponseType(typeof(ProjectServerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProjectServerDto>> Get(Guid projectId, Guid projectServerId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entity = await _projectServerRepository.GetByIdAsync(projectServerId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(ProjectServer), projectServerId);
            }

            return Ok(_mapper.Map<ProjectServerDto>(entity));
        }


        /// <summary>
        /// Get list of all project Servers for the specific project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ProjectServerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectServerDto>>> GetByUser(Guid projectId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var projectServers = await _projectServerRepository.GetAsync(p => p.ProjectId == projectId);

            return Ok(_mapper.Map<IEnumerable<ProjectServerDto>>(projectServers));
        }

        /// <summary>
        /// Creates a new project Server for the project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="request">Project Server data</param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create(string projectId, [FromBody] CreateProjectServerRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entity = _mapper.Map<ProjectServer>(request);

            var newEntity = await _projectServerRepository.AddAsync(entity);

            return Ok(newEntity.Id);
        }

        /// <summary>
        /// Update an existing project Server
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <param name="request">Project Server data</param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update(string projectId, [FromBody] UpdateProjectServerRequest request)
        {
            var entityProject = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), request.ProjectId);
            }

            var entityToUpdate = await _projectServerRepository.GetByIdAsync(request.ProjectServerId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(ProjectServer), request.ProjectServerId);
            }

            _mapper.Map(request, entityToUpdate, typeof(UpdateProjectServerRequest), typeof(ProjectServer));

            await _projectServerRepository.UpdateAsync(entityToUpdate);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project Server
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectServerId"></param>
        /// <returns></returns>
        [HttpDelete("{projectServerId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid projectId, Guid projectServerId)
        {
            var entityProject = await _projectRepository.GetByIdAsync(projectId);
            if (entityProject == null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            var entityToDelete = await _projectServerRepository.GetByIdAsync(projectServerId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(ProjectServer), projectServerId);
            }

            await _projectServerRepository.DeleteAsync(entityToDelete);
            return NoContent();
        }
    }
}