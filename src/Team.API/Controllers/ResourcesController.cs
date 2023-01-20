using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;
using Team.Application.Exceptions;
using Team.Domain.Entities;
using Team.Domain.Requests;
using Team.Infrastructure.Repositories;

namespace Team.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IResourceRepository _resourceRepository;

        public ResourcesController(IMapper mapper, IResourceRepository resourceRepository)
        {
            _mapper = mapper;
            _resourceRepository = resourceRepository;
        }

        /// <summary>
        /// Get list of all resource
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ResourceDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> Get()
        {
            var result = await _resourceRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ResourceDto>>(result));
        }

        /// <summary>
        /// Get a single specific Resource by it's unique id (GUID)
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [HttpGet("{resourceId}")]
        [ProducesResponseType(typeof(ProjectDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ResourceDto>> GetById(Guid resourceId)
        {
            var result = await _resourceRepository.GetByIdAsync(resourceId);
            if(result == null)
            {
                throw new NotFoundException(nameof(Resource), resourceId);
            }

            return Ok(_mapper.Map<ResourceDto>(result));
        }

        /// <summary>
        /// Creates / registers a new resource
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateResourceRequest request)
        {
            var entity = _mapper.Map<Resource>(request);

            var newEntity = await _resourceRepository.AddAsync(entity);

            return Ok(newEntity.Id);
        }

        /// <summary>
        /// Updates an existing Resource
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update([FromBody] UpdateResourceRequest request)
        {
            var entityToUpdate = await _resourceRepository.GetByIdAsync(request.ResourceId);
            if (entityToUpdate == null)
            {
                throw new NotFoundException(nameof(Resource), request.ResourceId);
            }

            await _resourceRepository.UpdateAsync(entityToUpdate);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing project
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [HttpDelete("{resourceId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid resourceId)
        {
            var entityToDelete = await _resourceRepository.GetByIdAsync(resourceId);
            if (entityToDelete == null)
            {
                throw new NotFoundException(nameof(Resource), resourceId);
            }

            await _resourceRepository.DeleteAsync(entityToDelete);
            return NoContent();
        }
    }
}
