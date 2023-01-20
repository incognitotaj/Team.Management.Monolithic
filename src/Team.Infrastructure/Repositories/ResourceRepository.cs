using Team.Application.Contracts.Persistence;
using Team.Domain.Entities;

namespace Team.Infrastructure.Repositories
{
    public class ResourceRepository : RepositoryBase<Resource>, IResourceRepository
    {
        public ResourceRepository(DataContext context)
            : base(context)
        {
        }
    }
}
