using System.Data.Entity;
using Elmah.EF;

namespace Elmah.EF6
{
    public class ElmahContext : DbContext
    {
        public ElmahContext() : this(ElmahHelper.GetConnectionString())
        {
        }

        public ElmahContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new ElmahDatabaseInitializer());
        }

        public virtual DbSet<ElmahError> ElmahErrors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ElmahErrorConfiguration());
        }
    }
}