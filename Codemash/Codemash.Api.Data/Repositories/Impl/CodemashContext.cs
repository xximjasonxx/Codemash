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
        public DbSet<Change> Changes { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // session
            modelBuilder.Entity<Session>().Property(s => s.Abstract).HasColumnType("ntext");
            modelBuilder.Entity<Session>().Property(s => s.Start).HasColumnType("datetime2");
            modelBuilder.Entity<Session>().Property(s => s.End).HasColumnType("datetime2");

            // speaker
            modelBuilder.Entity<Speaker>().Property(s => s.Biography).HasColumnType("ntext");

            // change
            modelBuilder.Entity<Change>().Property(c => c.Value).HasColumnType("ntext");
        }

        protected override bool ShouldValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        {
            return false;
        }
    }
}
