using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectResourceRepository : IAsyncRepository<ProjectResource>
    {
        // Get Resources by ProjectId
        Task<IEnumerable<ProjectResource>> GetByProjectIdAsync(Guid projectId);
    }
}
