using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IProjectRepository : IAsyncRepository<Project>
    {
        // Get Projects By User
        Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId);
    }
}
