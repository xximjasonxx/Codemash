using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISessionChangeRepository : IWriteRepository<SessionChange, int>, IReadRepository<SessionChange, int>
    {
    }
}
