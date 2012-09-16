using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISpeakerRepository : IWriteRepository<Speaker, int>, IReadRepository<Speaker, int>
    {
    }
}
