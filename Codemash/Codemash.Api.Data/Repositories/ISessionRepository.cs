using System.Collections.Generic;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISessionRepository : IReadRepository<Session, int>, IWriteRepository<Session, int>
    {
    }
}
