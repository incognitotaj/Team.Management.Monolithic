using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectMilestoneRepository : IAsyncRepository<ProjectMilestone>
    {
        // Get Milestones by ProjectId
        Task<IEnumerable<ProjectMilestone>> GetByProjectIdAsync(Guid projectId);
    }
}
