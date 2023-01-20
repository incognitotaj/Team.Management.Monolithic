using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId)
        {
            // Get projects for the selected user
            // Predicate: p => p.UserId == userId
            var projectsList = await _context.Projects
                                    .Where(p => p.Id == userId)
                                    .ToListAsync();

            return projectsList;
        }
    }
}
