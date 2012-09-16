using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories
{
    public interface ISpeakerChangeRepository : IReadRepository<SpeakerChange, int>, IWriteRepository<SpeakerChange, int>
    {
    }
}
