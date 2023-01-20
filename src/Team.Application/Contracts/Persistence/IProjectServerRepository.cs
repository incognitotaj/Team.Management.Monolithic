using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectServerRepository : IAsyncRepository<ProjectServer>
    {
        // Get Servers by ProjectId
        Task<IEnumerable<ProjectServer>> GetByProjectIdAsync(Guid projectId);
    }
}
