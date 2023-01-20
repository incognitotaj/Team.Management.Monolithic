using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectResourceDailyTaskRepository : IAsyncRepository<ProjectResourceDailyTask>
    {
        // Get Resource DailyTasks by Id
        Task<IEnumerable<ProjectResourceDailyTask>> GetByProjectResourceIdAsync(Guid projectResourceId);
    }
}
