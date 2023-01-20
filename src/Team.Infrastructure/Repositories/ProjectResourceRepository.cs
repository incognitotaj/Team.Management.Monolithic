using Microsoft.EntityFrameworkCore;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ProjectResourceRepository : RepositoryBase<ProjectResource>, IProjectResourceRepository
    {
        public ProjectResourceRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProjectResource>> GetByProjectIdAsync(Guid projectId)
        {
            var projectResources = await _context.ProjectResources
                                    .Where(p => p.ProjectId == projectId)
                                    .ToListAsync();

            return projectResources;
        }
    }
}
