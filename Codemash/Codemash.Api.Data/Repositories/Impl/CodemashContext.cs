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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // session
            modelBuilder.Entity<Session>().Property(s => s.Abstract).HasColumnType("text");
            modelBuilder.Entity<Session>().Property(s => s.Start).HasColumnType("datetime2");
            modelBuilder.Entity<Session>().Property(s => s.End).HasColumnType("datetime2");

            // speaker
            modelBuilder.Entity<Speaker>().Property(s => s.Biography).HasColumnType("text");

            // session changes
            modelBuilder.Entity<SessionChange>().Property(sc => sc.Value).HasColumnType("text");

            // speaker changes
            modelBuilder.Entity<SpeakerChange>().Property(sc => sc.Value).HasColumnType("text");
        }

        protected override bool ShouldValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        {
            return false;
        }
    }
}
