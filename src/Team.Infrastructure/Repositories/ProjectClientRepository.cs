using Microsoft.EntityFrameworkCore;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ProjectClientRepository : RepositoryBase<ProjectClient>, IProjectClientRepository
    {
        public ProjectClientRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProjectClient>> GetByProjectIdAsync(Guid projectId)
        {
            var projectClients = await _context.ProjectClients
                                    .Where(p => p.ProjectId == projectId)
                                    .ToListAsync();

            return projectClients;
        }
    }
}
