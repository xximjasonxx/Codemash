using System.Data.Entity;
using Codemash.Api.Data.Entities;

namespace Codemash.Api.Data.Repositories.Impl
{
    /// <summary>
    /// Context class allowing access to the database
    /// </summary>
    public class CodemashSessionsContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SessionChange> SessionChanges { get; set; }
    }
}
