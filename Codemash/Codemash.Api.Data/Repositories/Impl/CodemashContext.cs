using System.Data.Entity;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Repositories.Initialize;

namespace Codemash.Api.Data.Repositories.Impl
{
    /// <summary>
    /// Context class allowing access to the database
    /// We will use this to define the tables in the database
    /// </summary>
    public class CodemashContext : DbContext
    {
        public CodemashContext() : base("name=MainConnectionString")
        {
            Database.SetInitializer(new CodemashInitializer());
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<SessionChange> SessionChanges { get; set; }
        public DbSet<SpeakerChange> SpeakerChanges { get; set; } 
    }
}
