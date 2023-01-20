using Team.Domain.Entities;

namespace Team.Application.Contracts.Persistence
{
    public interface IResourceRepository : IAsyncRepository<Resource>
    {
    }
}
