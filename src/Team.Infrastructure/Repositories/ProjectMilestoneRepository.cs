using Microsoft.EntityFrameworkCore;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ProjectMilestoneRepository : RepositoryBase<ProjectMilestone>, IProjectMilestoneRepository
    {
        public ProjectMilestoneRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProjectMilestone>> GetByProjectIdAsync(Guid projectId)
        {
            var projectMilestones = await _context.ProjectMilestones
                                    .Where(p => p.ProjectId == projectId)
                                    .ToListAsync();

            return projectMilestones;
        }
    }
}
