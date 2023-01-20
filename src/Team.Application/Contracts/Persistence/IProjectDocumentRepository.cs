using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectDocumentRepository : IAsyncRepository<ProjectDocument>
    {
        // Get Documents by ProjectId
        Task<IEnumerable<ProjectDocument>> GetByProjectIdAsync(Guid projectId);
    }
}
