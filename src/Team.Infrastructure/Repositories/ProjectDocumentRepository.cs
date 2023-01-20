using Microsoft.EntityFrameworkCore;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ProjectDocumentRepository : RepositoryBase<ProjectDocument>, IProjectDocumentRepository
    {
        public ProjectDocumentRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProjectDocument>> GetByProjectIdAsync(Guid projectId)
        {
            var projectDocuments = await _context.ProjectDocuments
                                    .Where(p => p.ProjectId == projectId)
                                    .ToListAsync();

            return projectDocuments;
        }
    }
}
