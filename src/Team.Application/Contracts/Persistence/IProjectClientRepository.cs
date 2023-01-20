using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectClientRepository : IAsyncRepository<ProjectClient>
    {
        // Get Clients by ProjectId
        Task<IEnumerable<ProjectClient>> GetByProjectIdAsync(Guid projectId);
    }
}
