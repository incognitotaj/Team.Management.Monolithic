using Microsoft.EntityFrameworkCore;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ProjectResourceDailyTaskRepository : RepositoryBase<ProjectResourceDailyTask>, IProjectResourceDailyTaskRepository
    {
        public ProjectResourceDailyTaskRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProjectResourceDailyTask>> GetByProjectResourceIdAsync(Guid projectResourceId)
        {
            var projectResources = await _context.ProjectResourceDailyTasks
                                    .Where(p => p.ProjectResourceId == projectResourceId)
                                    .ToListAsync();

            return projectResources;
        }
    }
}
