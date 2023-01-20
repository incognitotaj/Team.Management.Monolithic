using Microsoft.EntityFrameworkCore;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ProjectServerRepository : RepositoryBase<ProjectServer>, IProjectServerRepository
    {
        public ProjectServerRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProjectServer>> GetByProjectIdAsync(Guid projectId)
        {
            var projectServers = await _context.ProjectServers
                                    .Where(p => p.ProjectId == projectId)
                                    .ToListAsync();

            return projectServers;
        }
    }
}
