using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Repositories.Impl;

namespace Codemash.Api.Data.Repositories.Initialize
{
    internal class CodemashInitializer : DropCreateDatabaseIfModelChanges<CodemashContext>
    {
    }
}
